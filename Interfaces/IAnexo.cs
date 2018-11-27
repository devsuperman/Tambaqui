using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tambaqui.Interfaces
{
    public interface IAnexo
    {        
        string Localizacao { get; set; }         
        string Nome { get; set; }
        Task SubstituirAnexo(IFormFile arquivo, IStorage storage); 
    }
}