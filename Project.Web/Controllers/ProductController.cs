using Microsoft.AspNetCore.Mvc;

namespace Project.Web.Controllers
{
    public class ProductController : Controller
    {   
        [Route("product")]
        public IActionResult Index()
        {
            return View(null);
        }        
        [Route("product/{x}")]
        public IActionResult Index(int? x = null)
        {
            return View(x);
        }
    }
}
