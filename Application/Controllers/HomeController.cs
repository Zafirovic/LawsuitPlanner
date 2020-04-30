using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> signInManager;
        public HomeController(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if(signInManager.IsSignedIn(User))
            {
                if(User.IsInRole("admin"))
                {
                    return RedirectToAction("ListUsers", "Administration");
                }
                else if(User.IsInRole("korisnik"))
                {
                    return RedirectToAction("LawsuitManagement", "User");
                }
                else 
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            
            return RedirectToAction("Login", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
