using Microsoft.AspNetCore.Mvc;

namespace OAuth.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignOut(string page)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}