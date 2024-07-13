using Microsoft.AspNetCore.Mvc;

namespace WarlockMVC.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
