using Application.Models;
using Application.Models.DataLayer;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{ 
    [Authorize]
    public class UserController : Controller
    {

        [HttpGet]
        public ViewResult LawsuitManagement()
        {
            return View();
        }

        [HttpGet]
        public ViewResult CalendarLawsuits()
        {
            return View("LawsuitCalendar");
        }
    }
}