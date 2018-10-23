using System;
using Tambaqui.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Administrativo.Services
{
    public class TiaIdentity
    {
        private readonly IHttpContextAccessor accessor;

        public TiaIdentity(IHttpContextAccessor _httpContextAccessor)
        {
            accessor = _httpContextAccessor;
        }

        public async Task LoginAsync(string usuario, string nome)
        {
            var claims = new List<Claim>
                {   
                    new Claim(ClaimTypes.Name, nome),                                     
                    new Claim(ClaimTypes.NameIdentifier, usuario)                    
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            await accessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }      

        public async Task LogoutAsync()
        {
            await accessor.HttpContext.SignOutAsync();
        }
    }
}