using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Tambaqui.Interfaces;
using System.IO;
using System;
using Tambaqui.Models;

namespace Tambaqui.Services
{
    public class DiscoRigido : IStorage
    {
        private const string caminho = "C:\\MeuSistema\\uploads";

        public async Task<byte[]> Download(string localizador)
        {
            if (string.IsNullOrWhiteSpace(localizador))
                throw new ArgumentNullException();

            var caminhoDaPasta = Path.Combine(caminho, localizador); 
            
            var arquivos = Directory.GetFiles(caminhoDaPasta);

            var caminhoDoArquivo = Path.Combine(caminhoDaPasta, arquivos[0]);
            
            byte[] bytes;
            
            using (var stream = File.Open(caminhoDoArquivo, FileMode.Open))
            {
                bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
            }

            return bytes;            
        }
        
        public Task Excluir(string localizador)
        {
            if (!string.IsNullOrWhiteSpace(localizador))
            {                
                var caminhoDaPasta = Path.Combine(caminho, localizador);                             

                if (Directory.Exists(caminhoDaPasta))
                    Directory.Delete(caminhoDaPasta, true);
            }

            return Task.CompletedTask;
        }

        public async Task SubstituirAnexo(Anexo anexo, IFormFile arquivoNovo)
        {
            anexo.Atualizar(nome: arquivoNovo.FileName, mime: arquivoNovo.ContentType);            
			await this.Excluir(anexo.Localizador);			
			await this.Upload(anexo.Localizador, arquivoNovo);
        }

        public async Task Upload(string localizador, IFormFile arquivo)
        {
            if (arquivo is null || string.IsNullOrWhiteSpace(localizador))
                throw new ArgumentNullException();

            var caminhoDaPasta = Path.Combine(caminho, localizador); 
            
            Directory.CreateDirectory(caminhoDaPasta);

            var caminhoDoArquivo =  Path.Combine(caminhoDaPasta, arquivo.FileName);            

            using (var stream = new FileStream(caminhoDoArquivo, FileMode.Create))
                await arquivo.CopyToAsync(stream);
       }
      
    }
}