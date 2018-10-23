using Tambaqui.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tambaqui.Interfaces
{
    public interface IStorage
    {
        Task Upload(IAnexo anexo, IFormFile arquivo);
        Task<byte[]> Download(IAnexo anexo);
        Task Excluir(IAnexo anexo);

        //Ao realizar upload sempre é necessário atribuir um localizador!!!
        // void AtribuirLocalizador(IAnexo anexo);
    }    
}