using Microsoft.AspNetCore.Mvc;

namespace Project.Web.Controllers
{
    public class ProdutoController : Controller
    {   
        [Route("produto")]
        public IActionResult Index()
        {
            return View(null);
        }        
        [Route("produto/{x}")]
        public IActionResult Index(int? x = null)
        {
            return View(x);
        }
    }
}
