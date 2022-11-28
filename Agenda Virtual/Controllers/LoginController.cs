using Agenda_Virtual.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Agenda_Virtual.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginView()
        {
            return View();
        }

        //Metodo de iniciar sesion
        [HttpGet]
        public IActionResult Login()
        {
            //Cookie de identificacion temporal
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index,Home");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            //Autentica el usuario
            if (login.Usuario == "Mario Alvarado" && login.Contraseña == "mariopedro")
            {
                //Identifica el usuaurio
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,login.Usuario ),
                    new Claim("OtherProerties","Example Role")

                };

                //Valida cookie
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = login.In
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                //Autoriza y direcciona
                return RedirectToAction("Index", "Home");
            }
            //Si no es valido no lo deja acceder por ningun lado
            else {
                ViewData["ValidateMessage"] = "Usuario no encontrado";
                return RedirectToAction("LoginView", "Login");
            }




        }


    }
}

