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
        private readonly ICodificador codificador;                  

      public TiaIdentity(IHttpContextAccessor _httpContextAccessor, ICodificador codificador)
        {
            this.httpContextAccessor = _httpContextAccessor;                        
            this.codificador = codificador;
        }

        internal async Task LoginAsync(string cpf, string nome, bool lembrar, bool ehAdmin)
        {
            var claims = new List<Claim>
                {   
                    new Claim(ClaimTypes.Name, nome),                                     
                    new Claim(ClaimTypes.NameIdentifier, cpf)                                        
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