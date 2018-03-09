using System.Linq;
using Tambaqui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using X.PagedList;

namespace Tambaqui.Controllers
{
    public class CoresController : Controller
    {
        private readonly Contexto db;

        public CoresController(Contexto contexto) => db = contexto;

        public async Task<IActionResult> Index(string search = "", int page = 1)
        {
            var lista = await db.Cores         
                .Where(w => w.Nome.Contains(search))                
                .OrderBy(w => w.Nome)
                .AsNoTracking()
                .ToPagedListAsync(page, 5);
          
            return View(lista);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var cor = await db.Cores.SingleOrDefaultAsync(m => m.Id == id);
            
            if (cor == null)            
                return NotFound();            

            return View(cor);
        }

        
        public ActionResult Create()
        {        
            return View();
        }

        

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Cor cor)
        {
            if (ModelState.IsValid)
            {
                db.Add(cor);
                
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(cor);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)            
                return NotFound();
            
            var cor = await db.Cores.SingleOrDefaultAsync(m => m.Id == id);
            
            if (cor == null)            
                return NotFound();
            
            return View(cor);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Cor cor)
        {
            if (id != cor.Id)            
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(cor);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!corExists(cor.Id))                    
                        return NotFound();                    
                    else                    
                        throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cor);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();
            
            var cor = await db.Cores.SingleOrDefaultAsync(m => m.Id == id);
            
            if (cor == null)            
                return NotFound();            

            return View(cor);
        }

        
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cor = await db.Cores.SingleOrDefaultAsync(m => m.Id == id);
            
            db.Cores.Remove(cor);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool corExists(int id)
        {
            return db.Cores.Any(e => e.Id == id);
        }
    }
}