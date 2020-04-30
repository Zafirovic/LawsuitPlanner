using Application.Models;
using Application.Models.DataLayer;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Authorize(Roles = "admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyController(ICompanyRepository repository)
        {
            this.companyRepository = repository;
        }

        [HttpGet]
        public ActionResult ListCompanies()
        {   
            CompanyViewModel model = new CompanyViewModel();
            model.companies = this.companyRepository.getAll();

            return View("../Administration/Companies", model);
        }

        [HttpPost]
        public Company CreateCompany(CompanyViewModel model)
        {
            var company = new Company() 
            { 
                name = model.name, 
                address = model.address
            };
            this.companyRepository.add(company);
            
            return company;
        }

        [HttpPost]
        public ActionResult DeleteCompany(int id)
        {
            if (companyRepository.get(id) != null)
                this.companyRepository.delete(id);

            return RedirectToAction("ListCompanies");
        }

        [HttpPost]
        public string EditCompany(CompanyViewModel model)
        {
            Company company = this.companyRepository.get(model.id);

            if (company != null)
            {
                if (company.name == model.name && company.address == model.address)
                    return "";

                company.name = model.name;
                company.address = model.address;
                this.companyRepository.update(company);
            }
            return "updated";
        }
    }
}