using Application.Models;
using Application.Models.DataAccess;
using Application.Models.DataLayer;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{ 
    [Authorize]
    public class LawsuitController : Controller
    {
        private readonly ILawsuitRepository lawsuitRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IContactRepository contactRepository;
        private readonly ITypeOfProcessRepository typeOfRepository;
        private readonly LawsuitDataContext context;
        private readonly UserManager<User> userManager;

        public LawsuitController(ILawsuitRepository lawRepository, ILocationRepository locRepository,
                              IContactRepository conRepository, ITypeOfProcessRepository tRepository,
                              LawsuitDataContext context, UserManager<User> userManager)
        {
            this.lawsuitRepository = lawRepository;
            this.locationRepository = locRepository;
            this.contactRepository = conRepository;
            this.typeOfRepository = tRepository;
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public ViewResult ListLawsuits()
        {
            LawsuitViewModel model = new LawsuitViewModel();
            model.lawsuits = this.lawsuitRepository.getForLawyer(userManager.GetUserId(HttpContext.User));
            model.contacts = this.contactRepository.getAll();
            model.locations = this.locationRepository.getAll("name_desc", "");
            model.processes = this.typeOfRepository.getAll();
            model.lawyers = context.Users.ToList();

            return View("../User/ListLawsuits", model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ViewResult ListLawsuitsAdmin()
        {
            LawsuitViewModel model = new LawsuitViewModel();
            model.lawsuits = this.lawsuitRepository.getAll();
            model.lawyers = context.Users.ToList();

            return View("../Administration/AdminListLawsuits", model);
        }


        [HttpGet]
        public IEnumerable<Lawsuit> ListLawsuitsCalendar()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            return this.lawsuitRepository.getForLawyer(userId);
        }

        [HttpPost]
        public LawsuitViewModel CreateLawsuit(LawsuitViewModel model, string[] lawyers)
        {
            DateTime dt;
            DateTime.TryParse(model.dateTimeOfEvent.ToString(), out dt);
            
            List<User> laws = context.Users.ToList();
            List<User> selectedLawyers = new List<User>();

            Lawsuit lawsuit = new Lawsuit
            {
                dateTimeOfEvent = dt,
                location = locationRepository.get(model.location),
                judge = contactRepository.get(model.judge),
                courtType = model.courtType,
                processId = model.processId,
                courtroomNumber = model.courtroomNumber,
                prosecutor = contactRepository.get(model.prosecutor),
                defendant = contactRepository.get(model.defendant),
                note = model.note,
                typeOfProcess = typeOfRepository.get(model.typeOfProcess),
            };
            lawsuitRepository.add(lawsuit);

            foreach(string lawyerId in lawyers)
            {
                context.LawsuitLawyers.Add(new LawsuitLawyer{ lawsuitId = lawsuit.id, userId = lawyerId });
                context.SaveChanges();
            }

            if (lawyers.Contains(userManager.GetUserId(HttpContext.User)))
            {
                model.locations = this.locationRepository.getAll("name_desc", "");
                model.contacts = this.contactRepository.getAll();
                model.courtTypes = Enum.GetValues(typeof(TipSuda)).Cast<TipSuda>()
                                    .ToDictionary(e => (int)e, e => e.ToString());
                model.processes = this.typeOfRepository.getAll();
                return model;
            }
            return null;
        }

        [HttpPost]
        public ActionResult DeleteLawsuit(int id)
        {
            if (lawsuitRepository.get(id) != null)
                this.lawsuitRepository.delete(id);

            return RedirectToAction("ListLawsuits");            
        }

        [HttpPost]
        public void EditLawsuit(LawsuitViewModel model)
        {
            Lawsuit lawsuit = lawsuitRepository.get(model.id);

            if (lawsuit != null)
            {
                lawsuit.dateTimeOfEvent = model.dateTimeOfEvent;
                lawsuit.location = locationRepository.get(model.location);
                lawsuit.judge = contactRepository.get(model.judge);
                lawsuit.courtType = model.courtType;
                lawsuit.processId = model.processId;
                lawsuit.courtroomNumber = model.courtroomNumber;
                lawsuit.prosecutor = contactRepository.get(model.prosecutor);
                lawsuit.defendant = contactRepository.get(model.defendant);
                lawsuit.note = model.note;
                lawsuit.typeOfProcess = typeOfRepository.get(model.typeOfProcess);

                this.lawsuitRepository.update(lawsuit);
            }
        }
    }
}