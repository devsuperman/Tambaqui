using Tambaqui.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System;

namespace Tambaqui.Models
{
    public class Anexo : IAnexo
    {
        public Anexo()
        {

            
        }
        public Anexo(IFormFile arquivo)
        {
            this.Nome = arquivo.FileName;
            this.Mime = arquivo.ContentType;
        }
        
        public int Id { get; set; }
        public string Mime { get; set; }
        public string Nome { get; set; }        
        public string LocalizadorDoAnexo { get; private set; } = Guid.NewGuid().ToString();
        public string Localizacao { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

       

        public async Task SubstituirAnexo(IFormFile novoArquivo, IStorage storage)
        {	
            this.Nome = novoArquivo.FileName;
            this.Mime = novoArquivo.ContentType;			
			await storage.Excluir(this);			
			await storage.Upload(this, novoArquivo);			
        }
    }
}