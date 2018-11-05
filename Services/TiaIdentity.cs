using System;
using Tambaqui.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Tambaqui.Interfaces;
using System.Text;

namespace Tambaqui.Services
{
    public class TiaIdentity
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEmail email;     
        private readonly ICodificador codificador;                  

        public TiaIdentity(IHttpContextAccessor _httpContextAccessor, IEmail email, ICodificador codificador)
        {
            this.httpContextAccessor = _httpContextAccessor;            
            this.email = email;
            this.codificador = codificador;
        }

        internal bool SenhaCorreta(string senhaDigitada, string senhaSalva)
        {   
            var senhaDigitadaCriptografada = codificador.GerarHash(senhaDigitada);
            return (senhaSalva == senhaDigitadaCriptografada);
        }

        public async Task LoginAsync(string usuario, string nome, bool lembrar, bool ehAdmin)
        {
            var claims = new List<Claim>
                {   
                    new Claim(ClaimTypes.Name, nome),                                     
                    new Claim(ClaimTypes.NameIdentifier, usuario)                    
                };

            if (ehAdmin)            
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));            

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);           


            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(5),
                IsPersistent = lembrar                
            };
            
            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }      

        public async Task LogoutAsync()
        {
            await httpContextAccessor.HttpContext.SignOutAsync();
        }

        internal async Task EnviarEmailParaCriacaoDeSenha(Usuario usuario)
        {
            string titulo = "Criação de Senha - Tambaqui";
            
            var mensagem = new StringBuilder();
            
            mensagem.Append("Acesse o seguinte link para criar sua senha: ");
            
            string linkTrocarSenha = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/Autenticacao/AlterarSenha/{usuario.Hash}";
            
            mensagem.Append(linkTrocarSenha);

            await email.EnviarAsync(usuario.Email, titulo, mensagem.ToString());
        }

        internal async Task EnviarEmailParaTrocaDeSenha(Usuario usuario)
        {
            string titulo = "Alteração de Senha - Tambaqui";
            
            var mensagem = new StringBuilder();
            mensagem.Append("Acesse o seguinte link para alterar sua senha: ");

            string linkTrocarSenha = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/Autenticacao/AlterarSenha/{usuario.Hash}";            

            mensagem.Append(linkTrocarSenha);

            await email.EnviarAsync(usuario.Email, titulo, mensagem.ToString());
        }
    }
}