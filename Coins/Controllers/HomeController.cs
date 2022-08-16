using Microsoft.AspNetCore.Mvc;
namespace Coins.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("/swagger");
        }
    }
}