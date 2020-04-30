using Application.Models.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModel;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Application.Controllers
{
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        private readonly IContactRepository contactRepository;
        private readonly ICompanyRepository companyRepository;

        public ContactController(IContactRepository conRepository, ICompanyRepository compRepository)
        {
            this.contactRepository = conRepository;
            this.companyRepository = compRepository;
        }

        [HttpGet]
        public ActionResult ListContacts(string sortOrder, string SearchString)
        {   
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = String.IsNullOrEmpty(SearchString) ? "" : SearchString;

            ContactCompanyViewModel model = new ContactCompanyViewModel();
            model.contacts = this.contactRepository.getAll(sortOrder, SearchString);
            model.companies = this.companyRepository.getAll();

            return View("../Administration/Contact", model);
        }
    
        [HttpPost]
        public ContactCompanyViewModel CreateContact(ContactCompanyViewModel model)
        {
            Contact contact = new Contact()
            {
                name = model.name + " " + model.surname,
                phone1 = model.phone1,
                phone2 = model.phone2,
                address = model.address,
                email = model.email,
                legalPerson = model.legalPerson,
                job = model.job,
                company = companyRepository.get(model.companyId),
            };

            this.contactRepository.add(contact);
            model.companies = this.companyRepository.getAll();
            model.name = contact.name;

            return model;
        }

        [HttpPost]
        public ActionResult DeleteContact(int id)
        {
            if (contactRepository.get(id) != null)
                this.contactRepository.delete(id);

            return RedirectToAction("ListContacts");
        }

        [HttpPost]
        public void EditContact(ContactCompanyViewModel model)
        {
            Contact contact = contactRepository.get(model.id);

            if (contact != null)
            {
                contact.name = model.name;
                contact.phone1 = model.phone1;
                contact.phone2 = model.phone2;
                contact.address = model.address;
                contact.email = model.email;
                contact.legalPerson = model.legalPerson;
                contact.job = model.job;
                contact.company = companyRepository.get(model.companyId) == null ? null : companyRepository.get(model.companyId);

                this.contactRepository.update(contact);
            }
        }
    }
}