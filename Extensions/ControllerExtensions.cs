using Microsoft.AspNetCore.Mvc;

namespace Tambaqui.Extensions
{
    public static class ControllerExtensions
    {
        public static void NotificarSucesso(this Controller controller, string mensagem = "Operação concluída com sucesso.")
        {   
            controller.TempData["notificacao"] = mensagem;
        }        
    }
}