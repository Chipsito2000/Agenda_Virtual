using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda_Virtual.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Contacto()
        {
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }
        

        public IActionResult Index()
        {
            return View();
        }

        //Metodo para salir de la sesion
        public async Task<IActionResult> LogOut()
        {
            //Autorizacion por cookie del usuario
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginView", "Login");
        }



    }
}