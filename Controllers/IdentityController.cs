using Microsoft.AspNetCore.Mvc;
using Frontend.Areas.Identity.Pages;
namespace Frontend.Controllers
{
    public class IdentityController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
