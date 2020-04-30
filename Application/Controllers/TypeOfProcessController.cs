using System;
using Application.Models;
using Application.Models.DataLayer;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Authorize(Roles = "admin")]
    public class TypeOfProcessController : Controller
    {
        private readonly ITypeOfProcessRepository processRepository;
        
        public TypeOfProcessController(ITypeOfProcessRepository repository)
        {
            this.processRepository = repository;
        }

        [HttpGet]
        public ActionResult ListTypeOfProcess(string sortOrder, string SearchString)
        {   
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = String.IsNullOrEmpty(SearchString) ? "" : SearchString;

            TypeOfProcessViewModel model = new TypeOfProcessViewModel();
            model.processes = this.processRepository.getAll(sortOrder, SearchString);

            return View("../Administration/TypeOfProcess", model);
        }

        [HttpPost]
        public int CreateTypeOfProcess(string name)
        {
            var typeP = new TypeOfProcess() { name = name };
            this.processRepository.add(typeP);
            
            return typeP.id;
        }

        [HttpPost]
        public ActionResult DeleteTypeOfProcess(int id)
        {
            if (processRepository.get(id) != null)
                this.processRepository.delete(id);

            return RedirectToAction("ListTypeOfProcess");
        }

        [HttpPost]
        public string EditTypeOfProcess(int id, string processName)
        {
            TypeOfProcess tipP = processRepository.get(id);

            if(tipP.name == processName)
                return "";

            if (tipP != null)
            {
                tipP.name = processName;
                this.processRepository.update(tipP);
            }
            return processName;
        }

        [AcceptVerbs("Post", "Get")]
        public ActionResult UsedTypeOfProcess(string processName)
        {
            var process = processRepository.get(processName);
            
            return process == null ? Json(true) : Json($"Grad sa nazivom {processName} vec postoji");
        }
    }
}