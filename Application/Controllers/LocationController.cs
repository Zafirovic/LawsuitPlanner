using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Application.Models.DataLayer;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace Application.Controllers
{
    [Authorize(Roles = "admin")]
    public class LocationController : Controller
    {
        private readonly ILocationRepository locationRepository;

        public LocationController(ILocationRepository repository)
        {
            this.locationRepository = repository;
        }

        [HttpGet]
        public ActionResult ListLawsuitCities(string sortOrder, string SearchString)
        {   
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = SearchString;

            LocationViewModel model = new LocationViewModel();
            model.locations = this.locationRepository.getAll(sortOrder, SearchString);

            return View("../Administration/LawsuitCities", model);
        }

        [HttpPost]
        public int CreateLocation(string cityName)
        {
            var lok = new Location() { cityName = cityName };
            this.locationRepository.add(lok);
            
            return lok.id;
        }

        [HttpPost]
        public ActionResult DeleteLocation(int id)
        {
            if (locationRepository.get(id) != null)
                this.locationRepository.delete(id);

            return RedirectToAction("ListLawsuitCities");
        }

        [HttpPost]
        public string EditLocation(int id, string cityName)
        {
            Location lok = locationRepository.get(id);

            if (lok != null)
            {
                if(cityName == lok.cityName || cityName == "")
                return "";

                lok.cityName = cityName;
                this.locationRepository.update(lok);
            }
            return cityName;
        }

        [AcceptVerbs("Post", "Get")]
        public ActionResult UsedName(string cityName)
        {
            var grad = locationRepository.get(cityName);
            
            return grad == null ? Json(true) : Json($"Grad sa nazivom {cityName} vec postoji");
        }
    }
}