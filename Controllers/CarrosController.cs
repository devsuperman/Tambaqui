using System.Linq;
using X.PagedList;
using Tambaqui.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tambaqui.Controllers
{
    public class CarrosController : Controller
    {
        private readonly Contexto db;

        public CarrosController(Contexto contexto) => db = contexto;

        public async Task<IActionResult> Index(string search = "", int page = 1)
        {
            var lista = await db.Carros
                .Include(q => q.Cor)
                .Where(w => 
                    w.Modelo.Contains(search) || 
                    w.Cor.Nome.Contains(search)
                )
                .OrderBy(a => a.Modelo)
                .ThenBy(a => a.Cor.Nome)
                .AsNoTracking()
                .ToPagedListAsync(page, 4);

            return View(lista);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var carro = await db.Carros.Include(q => q.Cor).SingleOrDefaultAsync(m => m.Id == id);
            
            if (carro == null)            
                return NotFound();            

            return View(carro);
        }

        
        public async Task<IActionResult> Create()
        {
            await SetarViewbags();
            return View();
        }

        private async Task SetarViewbags()
        {            
            ViewData["Cores"] = new SelectList(await db.Cores.ToListAsync(), "Id", "Nome" );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Modelo,CorId")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                db.Add(carro);                

                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }            
            await SetarViewbags();
            return View(carro);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)            
                return NotFound();
            
            var carro = await db.Carros.SingleOrDefaultAsync(m => m.Id == id);
            
            if (carro == null)            
                return NotFound();
            
            await SetarViewbags();
            return View(carro);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Modelo,CorId")] Carro carro)
        {
            if (id != carro.Id)            
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {                    
                    db.Update(carro);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarroExists(carro.Id))                    
                        return NotFound();                    
                    else                    
                        throw;                    
                }
                return RedirectToAction(nameof(Index));
            }
            await SetarViewbags();
            return View(carro);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();
            
            var carro = await db.Carros.Include(q => q.Cor).SingleOrDefaultAsync(m => m.Id == id);
            
            if (carro == null)            
                return NotFound();            

            return View(carro);
        }

        
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carro = await db.Carros.Include(q => q.Cor).SingleOrDefaultAsync(m => m.Id == id);
            
            db.Carros.Remove(carro);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CarroExists(int id)
        {
            return db.Carros.Any(e => e.Id == id);
        }
    }
}