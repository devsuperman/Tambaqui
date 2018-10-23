using Tambaqui.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System;

namespace Tambaqui.Models
{
    public class Anexo : IAnexo
    {
        [MaxLength(50), Key]
        public string Localizacao { get; set; } 

        [MaxLength(50)]
        public string Nome { get; set; }

        public async Task SalvarArquivo(IFormFile arquivo, IStorage storage)
        {   
            //Se for editar, só altera o arquivo, a localização é a mesma.
            if (string.IsNullOrWhiteSpace(Localizacao))            
                Localizacao = Guid.NewGuid().ToString();                

            Nome = arquivo.FileName;
            await storage.Upload(this, arquivo);
        }
    }
}