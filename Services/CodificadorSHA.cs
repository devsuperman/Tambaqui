using System;
using System.Security.Cryptography;
using System.Text;
using Tambaqui.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Tambaqui.Services
{
    public class CodificadorSHA : ICodificador
    {
        public string GerarHash(string txt)
        {            
            var algoritmo = SHA512.Create();
            var senhaEmBytes = Encoding.UTF8.GetBytes(txt);
            var senhaCifrada = algoritmo.ComputeHash(senhaEmBytes);
            
            var sb = new StringBuilder();
            
            foreach (var caractere in senhaCifrada)            
                sb.Append(caractere.ToString("X2"));
            
            return sb.ToString();
        }       
    }
}