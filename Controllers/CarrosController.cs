using System.Linq;
using X.PagedList;
using Tambaqui.Models;
using Tambaqui.Helpers;
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
        private readonly SelectListTop selectListTop;

        public CarrosController(Contexto db, SelectListTop selectListTop)
        {
            this.db = db;
            this.selectListTop = selectListTop;
        } 

        public async Task<IActionResult> Index(string search = "", int page = 1)
        {                        
            search = search is null ? "" : search;
            
            var lista = await db.Carros
                .Include(q => q.Cor)
                .Where(w => 
                    w.Modelo.Contains(search) || 
                    w.Cor.Nome.Contains(search)
                )
                .OrderBy(a => a.Modelo)
                .ThenBy(a => a.Cor.Nome)
                .AsNoTracking()
                .ToPagedListAsync(page, PaginacaoHelper.TamanhoDePaginaPadrao);

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

        
        public async Task<IActionResult> Create()
        {
            await CarregarViewbags();
            return View();
        }

        private async Task CarregarViewbags()
        {            
            ViewBag.Cores = await selectListTop.Cores();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Carro carro)
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
        
        public async Task<IActionResult> Edit(int id)
        {            
            var carro = await db.Carros.SingleOrDefaultAsync(m => m.Id == id);
            
            if (carro is null)            
                return NotFound();
            
            await CarregarViewbags();
            return View(carro);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Carro carro)
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