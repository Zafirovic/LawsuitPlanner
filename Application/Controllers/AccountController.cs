using System.Threading.Tasks;
using Application.Models;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnURL)
        {
            if(ModelState.IsValid)
            {
                var user = new User
                {
                    name = model.name,
                    surname = model.surname,
                    UserName = model.email,
                    Email = model.email
                };

                var result = await userManager.CreateAsync(user, model.repeatPassword);
                

                if (result.Succeeded)
                {
                    result = await userManager.UpdateSecurityStampAsync(user);
                    result = await userManager.AddToRoleAsync(user, "korisnik");

                    return RedirectToAction("listusers", "administration");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [AcceptVerbs("Post", "Get")]
        public async Task<IActionResult> UsedEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            
            return user == null ? Json(true) : Json($"Email {email} se vec koristi");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                    return RedirectToAction("ListUsers", "Administration");
                else
                    return RedirectToAction("LawsuitManagement", "User");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.email, model.password, model.rememberMe, lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(model.email);
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles.Contains("admin"))
                        return RedirectToAction("ListUsers", "Administration");
                    else
                        return RedirectToAction("LawsuitManagement", "User");
                }

                ModelState.AddModelError(string.Empty, "Pogresan unos prilikom logovanja ili nalog nema pristupa jos uvek");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}