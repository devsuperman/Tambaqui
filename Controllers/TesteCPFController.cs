using Tambaqui.Models;
using Microsoft.AspNetCore.Mvc;
using Tambaqui.ViewModels;

namespace Tambaqui.Controllers
{
    public class TesteCPFController : Controller
    {
        private readonly Contexto db;

        public TesteCPFController(Contexto contexto) => db = contexto;

        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Index(CPFVM model)
        {            
            if (ModelState.IsValid)
            {                

            }
            
            return View(model);
        }
       
    }
}