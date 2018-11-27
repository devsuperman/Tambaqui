using System.Linq;

using Tambaqui.Models;

using Tambaqui.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tambaqui.Controllers
{
    public class CarrosController : Controller
    {
        private readonly Contexto db;
        private readonly GeradorDeListas selectListTop;

        public CarrosController(Contexto db, GeradorDeListas selectListTop)
        {
            this.db = db;
            this.selectListTop = selectListTop;
        } 

        public async Task<IActionResult> Index()
        {                        
            var lista = await db.Carros
                .Include(q => q.Cor)                                
                .AsNoTracking()
                .ToListAsync();

            return View(lista);
        }


        public async Task<IActionResult> Details(int id)
        {
            var carro = await db.Carros
                .Include(q => q.Cor)
                .SingleOrDefaultAsync(m => m.Id == id);
            
            if (carro is null)            
                return NotFound();            

            return View(carro);
        }

        
        public async Task<IActionResult> Criar()
        {
            await CarregarViewbags();
            return View();
        }

        private async Task CarregarViewbags()
        {            
            ViewBag.Cores = await selectListTop.Cores();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Carro carro)
        {
            if (ModelState.IsValid)
            {
                db.Add(carro);                

                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }            
            await CarregarViewbags();
            return View(carro);
        }
        
        public async Task<IActionResult> Editar(int id)
        {            
            var carro = await db.Carros.SingleOrDefaultAsync(m => m.Id == id);
            
            if (carro is null)            
                return NotFound();
            
            await CarregarViewbags();
            return View(carro);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Carro carro)
        {   
            if (ModelState.IsValid)
            {
                db.Update(carro);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await CarregarViewbags();
            return View(carro);
        }
        
    }
}