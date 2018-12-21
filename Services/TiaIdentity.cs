using System;
using Tambaqui.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Tambaqui.Interfaces;

namespace Tambaqui.Services
{
    public class TiaIdentity
    {
        private readonly IHttpContextAccessor httpContextAccessor;        
        private readonly ICodificador codificador;                  

      public TiaIdentity(IHttpContextAccessor _httpContextAccessor, ICodificador codificador)
        {
            this.httpContextAccessor = _httpContextAccessor;                        
            this.codificador = codificador;
        }

        internal async Task LoginAsync(Usuario usuario, bool lembrar)
        {
            var claims = new List<Claim>
                {   
                    new Claim(ClaimTypes.Name, usuario.Nome),                                     
                    new Claim(ClaimTypes.NameIdentifier, usuario.CPF),
                    new Claim(ClaimTypes.Role, usuario.Perfil)                                                        
                };

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
        }


        internal bool SenhaCorreta(string senhaDigitada, string senhaSalva)
        {   
            var senhaDigitadaCriptografada = codificador.GerarHash(senhaDigitada);
            return (senhaSalva == senhaDigitadaCriptografada);
        }


        internal async Task LogoutAsync()
        {
            await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        } 
    }
}