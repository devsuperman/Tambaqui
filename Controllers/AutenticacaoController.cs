using System;
using System.Linq;
using Tambaqui.Models;
using System.Threading.Tasks;
using Tambaqui.Services;
using Microsoft.AspNetCore.Mvc;
using Tambaqui.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Tambaqui.Interfaces;

namespace Tambaqui.Controllers
{    
    public class AutenticacaoController : Controller
    {
        private readonly Contexto db;
        private readonly TiaIdentity.Autenticador tiaIdentity;        
        private readonly ICodificador codificador;
        private readonly IEmail servicoDeEmail;

        public AutenticacaoController(Contexto db, TiaIdentity.Autenticador tiaIdentity, ICodificador codificador, IEmail servicoDeEmail)
        {
            this.db = db;
            this.servicoDeEmail = servicoDeEmail;
            this.tiaIdentity = tiaIdentity;            
            this.codificador = codificador;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewmodel)
        {   
            var usuario = await db.Usuarios.FirstOrDefaultAsync(a => a.CPF == viewmodel.Usuario && a.Ativo);
            
            var cpfOuSenhaIncorretos = (usuario == null) || !(usuario.SenhaCorreta(viewmodel.Senha));
                
            if (cpfOuSenhaIncorretos)            
                ModelState.AddModelError("", "Usuário ou Senha incorretos!");

            if (ModelState.IsValid)
            {                
                await tiaIdentity.LoginAsync(usuario, viewmodel.Lembrar);
                return RedirectToAction("Index","Home");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await tiaIdentity.LogoutAsync();
            return View(nameof(Login));
        }

        public ActionResult EsqueciMinhaSenha() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EsqueciMinhaSenha(string email)
        {
            var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Ativo);

            if (usuario is null)
                return NotFound();
            
            usuario.GerarNovaHash();

            db.Update(usuario);
            await db.SaveChangesAsync();

            await servicoDeEmail.EnviarEmailParaTrocaDeSenha(usuario.Email, usuario.Hash);                

            return Ok();
        }

         public async Task<IActionResult> AlterarSenha(string id)
        {
            
            var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Hash == id);

            if (usuario == null || usuario.HashUtilizado)
            {
                //this.Error("Este link está expirado.");
                return RedirectToAction(nameof(Login));
            }

            var viewModel = new AlterarSenhaVM(usuario);

            return View(viewModel);
           
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Hash == viewModel.Id);

                if (usuario == null || usuario.HashUtilizado)
                {
                    //this.Atencao("Link expirado!");
                    return RedirectToAction(nameof(Login));
                }
                                
                usuario.AlterarSenha(viewModel.NovaSenha);
                usuario.UtilizarHash();
                
                db.Update(usuario);
                await db.SaveChangesAsync();

                //this.Sucesso("Senha alterada com sucesso!");
                return RedirectToAction(nameof(Login));
            }

            return View(viewModel);
        }

    }
    
}