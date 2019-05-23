using Tambaqui.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace Tambaqui.Models
{
    public abstract class Anexo
    {
        public Anexo() {}
        
        public Anexo(IFormFile arquivo)
        {
            this.Nome = arquivo.FileName;
            this.Mime = arquivo.ContentType;
        }
        
        public string Mime { get; private set; }
        public string Nome { get; private set; }        
        public string Localizador { get; private set; } = Guid.NewGuid().ToString();

        public async Task SubstituirAnexo(IFormFile novoArquivo, IStorage storage)
        {	
            this.Nome = novoArquivo.FileName;
            this.Mime = novoArquivo.ContentType;			
			await storage.Excluir(this.Localizador);			
			await storage.Upload(this.Localizador, novoArquivo);			
        }

        internal void Atualizar(string nome, string mime)
        {
            this.Nome = nome;
            this.Mime = mime;
        }
    }
}