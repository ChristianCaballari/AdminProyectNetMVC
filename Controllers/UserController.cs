using AdminProyectos.Extensions;
using AdminProyectos.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminProyectos.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager,
            SignInManager<User> signInManager) {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        { 
          if(!ModelState.IsValid) { return View(model); }

            var user = new User() { Email = model.Email };

            var result = await _userManager.CreateAsync(user,password: model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                //TODO: Agreagar notificaiones
                 CustomNotification("Bienvenido al Sistema", NotificationType.Success, user.Email);
                return RedirectToAction("Index", "Proyect");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
       [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //TODO: Agreagar notificaiones
                 CustomNotification("Bienvenido al Sistema", NotificationType.Success, model.Email);
                return RedirectToAction("Index", "Proyect");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Credenciales Incorrectas");
                return View(model);
            }     
        }
        [HttpPost]
        public async Task<IActionResult> Logout() 
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Proyect");

        }
    }
}
