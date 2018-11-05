using System;
using System.Linq;
using Tambaqui.Models;
using System.Threading.Tasks;
using Tambaqui.Services;
using Microsoft.AspNetCore.Mvc;
using Tambaqui.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Tambaqui.Controllers
{    
    // [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        private readonly Contexto db;
        private readonly TiaIdentity tiaIdentity;

        public UsuariosController(Contexto db, TiaIdentity tiaIdentity)
        {
            this.db = db;
            this.tiaIdentity = tiaIdentity;
        }        
        
        public async Task<IActionResult> Index()
        {
            var usuarios = await db.Usuarios
                .AsNoTracking()
                .ToListAsync();

            return View(usuarios);
        }

        
        public IActionResult Criar()
        {
            return View();
        }       

        [HttpPost]
        public async Task<IActionResult> Criar(UsuarioVM viewModel)
        {
            var CPFJaExiste = await db.Usuarios.AnyAsync(a => a.CPF == viewModel.CPF);
            var EmailJaExiste = await db.Usuarios.AnyAsync(a => a.Email == viewModel.Email);
            
            if(CPFJaExiste)
                ModelState.AddModelError("CPF", "CPF já foi cadastrado!");

            if(EmailJaExiste)
                ModelState.AddModelError("Email", "Email já foi cadastrado!");

            if (ModelState.IsValid)
            {   
                var usuario = new Usuario(viewModel.Nome, viewModel.CPF, viewModel.Email);
                await db.AddAsync(usuario);
                await db.SaveChangesAsync();
                
                await tiaIdentity.EnviarEmailParaCriacaoDeSenha(usuario);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await db.Usuarios.FindAsync(id);

            if (usuario == null)            
                NotFound();            

            var viewModel = new UsuarioVM(usuario);

            return View(viewModel);
        }       

        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioVM model)
        {   
            var CPFJaExisteEmOutroUsuario = await db.Usuarios.AnyAsync(a =>
                a.Id != model.Id && 
                a.CPF == model.CPF);

            var EmailJaExisteEmOutroUsuario = await db.Usuarios.AnyAsync(a =>
                a.Id != model.Id && 
                a.Email == model.Email);
            
            if(CPFJaExisteEmOutroUsuario)
                ModelState.AddModelError("CPF", "CPF já foi cadastrado!");

            if(EmailJaExisteEmOutroUsuario)
                ModelState.AddModelError("Email", "Email já foi cadastrado!");

            if (ModelState.IsValid)
            {                
                var usuario = await db.Usuarios.FindAsync(model.Id);

                usuario.Atualizar(model.Nome, model.CPF, model.Email);
                db.Update(usuario);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> InverterAtivo(int id)
        {
            var model = await db.Usuarios.FindAsync(id);

            if (model is null)
                return NotFound();

            model.InverterAtivo();
            
            db.Update(model);
            await db.SaveChangesAsync();

            return Ok();
        }

        
       
    }
    
}