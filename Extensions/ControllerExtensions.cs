using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Tambaqui.Extensions
{
    public static class ControllerExtensions
    {
        public static void NotificarSucesso(this Controller controller, string mensagem = "Operação concluída com sucesso.")
        {   
            controller.TempData["notificacao"] = mensagem;
        }        
        
        public static int IdDoUsuarioLogado(this Controller controller)
        {
            var id = controller.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value;
            return Convert.ToInt32(id);
        }
    }
}