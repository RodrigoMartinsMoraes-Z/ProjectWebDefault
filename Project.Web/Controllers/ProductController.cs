using Microsoft.AspNetCore.Mvc;

namespace Project.Web.Controllers
{
    public class ProductController : Controller
    {   
        [Route("product")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return View(null);
        }        
        [Route("product/{x}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index(int? x = null)
        {
            return View(x);
        }
    }
}
