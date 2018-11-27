using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Tambaqui.Interfaces;
using Tambaqui.Models;
using System.IO;
using System;

namespace Tambaqui.Services
{
    public class DiscoRigido : IStorage
    {
        private const string caminho = "C:\\DER-RO\\uploads";

       
        public async Task<byte[]> Download(IAnexo anexo)
        {   
             if (string.IsNullOrWhiteSpace(anexo.Localizacao))
                throw new ArgumentNullException();

            var caminhoDaPasta = Path.Combine(caminho, anexo.Localizacao); 
            
            var arquivos = Directory.GetFiles(caminhoDaPasta);

            var caminhoDoArquivo = Path.Combine(caminhoDaPasta, arquivos[0]);

            return await File.ReadAllBytesAsync(caminhoDoArquivo);   
        }

        public Task Excluir(IAnexo anexo)
        {
             if (!string.IsNullOrWhiteSpace(anexo.Localizacao))
            {                
                var caminhoDaPasta = Path.Combine(caminho, anexo.Localizacao);                             

                if (Directory.Exists(caminhoDaPasta))
                    Directory.Delete(caminhoDaPasta, true);
            }

            return Task.CompletedTask;
        }

        public async Task Upload(IAnexo anexo, IFormFile arquivo)
        {
           if (arquivo is null || string.IsNullOrWhiteSpace(anexo.Localizacao))
                throw new ArgumentNullException();

            var caminhoDaPasta = Path.Combine(caminho, anexo.Localizacao); 
            
            Directory.CreateDirectory(caminhoDaPasta);

            var caminhoDoArquivo =  Path.Combine(caminhoDaPasta, arquivo.FileName);            

            using (var stream = new FileStream(caminhoDoArquivo, FileMode.Create))
                await arquivo.CopyToAsync(stream);

        }        
    }
}