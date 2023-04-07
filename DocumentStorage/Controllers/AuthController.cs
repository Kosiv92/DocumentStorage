using DocumentStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStorage.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
