using System.Linq;
using X.PagedList;
using Tambaqui.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            
            return View();
        }
       
    }
}