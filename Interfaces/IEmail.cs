using System.Threading.Tasks;

namespace Tambaqui.Interfaces
{
    public interface IEmail
    {
        Task EnviarAsync(string destino, string assunto, string mensagem);
    }
}