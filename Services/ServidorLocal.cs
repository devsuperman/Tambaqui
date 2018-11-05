using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Tambaqui.Interfaces;
using Tambaqui.Models;
using System.IO;
using System;

namespace Tambaqui.Services
{
    public class ServidorLocal : IStorage
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ServidorLocal(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<byte[]> Download(IAnexo anexo)
        {   
            return await File.ReadAllBytesAsync(anexo.Localizacao);                
        }

        public Task Excluir(IAnexo anexo)
        {
            var caminhoDaPasta = Path.GetDirectoryName(anexo.Localizacao);
            
            if(Directory.Exists(caminhoDaPasta))
                Directory.Delete(caminhoDaPasta, true);  

            return Task.CompletedTask;
        }

        public async Task Upload(IAnexo anexo, IFormFile arquivo)
        {
            var pastaRaiz = _hostingEnvironment.WebRootPath;
            
            //Foi necessária a criação de uma pasta com GUID para que não houvessem problemas com arquivos com o mesmo nome
            var pastaDoArquivo = Path.Combine(pastaRaiz, "uploads", Guid.NewGuid().ToString()); 
            
            Directory.CreateDirectory(pastaDoArquivo);

            var Localizacao = Path.Combine(pastaDoArquivo, arquivo.FileName);            
            
            if (arquivo.Length > 0)
            {
                using (var stream = new FileStream(Localizacao, FileMode.Create))                
                    await arquivo.CopyToAsync(stream);                
            }

        }        
    }
}