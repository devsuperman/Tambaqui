using Tambaqui.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tambaqui.Interfaces
{
    public interface IStorage
    {
        Task Upload(string localizador, IFormFile arquivo);
        Task<byte[]> Download(string localizador);        
        Task SubstituirAnexo(Anexo anexo, IFormFile arquivoNovo);
        Task Excluir(string localizador);      
    }    
}