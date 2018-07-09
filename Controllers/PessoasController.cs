using System.Linq;
using X.PagedList;
using Tambaqui.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tambaqui.Helpers;

namespace Tambaqui.Controllers
{
    public class PessoasController : Controller
    {
        private readonly Contexto db;

        public PessoasController(Contexto contexto) => db = contexto;

        public async Task<IActionResult> Index(string search = "", int page = 1)
        {                        
            search = search is null ? "" : search;
            
            var lista = await db.Pessoas                
                .Where(w => 
                    w.Nome.ToLower().Contains(search) || 
                    w.Email.ToLower().Contains(search)
                )
                .OrderBy(a => a.Nome)                
                .AsNoTracking()
                .ToPagedListAsync(page, PaginacaoHelper.TamanhoDePaginaPadrao);

            return View(lista);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return BadRequest();            

            var model = await db.Pessoas.SingleOrDefaultAsync(m => m.Id == id);
            
            if (model == null)            
                return NotFound();            

            return View(model);
        }

        
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pessoa model)
        {
            if (ModelState.IsValid)
            {
                db.Add(model);                

                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }            
            
            return View(model);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)            
                return NotFound();
            
            var model = await db.Pessoas.SingleOrDefaultAsync(m => m.Id == id);
            
            if (model == null)            
                return NotFound();
                        
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pessoa model)
        {   
            if (ModelState.IsValid)
            {
                db.Update(model);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }
        
    }
}