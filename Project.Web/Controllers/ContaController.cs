using Microsoft.AspNetCore.Mvc;

namespace Project.Web.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

    }
}
