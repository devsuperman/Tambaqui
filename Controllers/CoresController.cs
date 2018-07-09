using System.Linq;
using Tambaqui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using X.PagedList;
using Tambaqui.Helpers;

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
                .ToPagedListAsync(page, PaginacaoHelper.TamanhoDePaginaPadrao);
          
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
        public async Task<IActionResult> Create(Cor cor)
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
                return BadRequest();
            
            var cor = await db.Cores.SingleOrDefaultAsync(m => m.Id == id);
            
            if (cor == null)            
                return NotFound();
            
            return View(cor);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cor cor)
        {           
            
            if (ModelState.IsValid)
            {
                
                db.Update(cor);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(cor);
        }     

    }
}