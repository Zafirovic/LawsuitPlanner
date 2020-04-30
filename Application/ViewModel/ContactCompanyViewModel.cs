using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewModel
{
    public class ContactCompanyViewModel
    {
        public ContactCompanyViewModel()
        {
            contacts = new List<Contact>();
            companies = new List<Company>();
        }

        [HiddenInput]
        public int id { get; set;}

        [Required(ErrorMessage="Potrebno je da unesete ime kontakta")]
        [Display(Name = "Ime")]
        public string name { get; set; }

        [Required(ErrorMessage="Potrebno je da unesete prezime kontakta")]
        [Display(Name = "Prezime")]
        public string surname { get; set; }

        public string phone1 { get; set; }

        public string phone2 { get; set; }

        public string address { get; set; }

        [EmailAddress]
        public string email { get; set; }

        public bool legalPerson { get; set; }

        public string job { get; set; }

        public int companyId { get; set; }

        public IEnumerable<Contact> contacts { get; set; }

        public IEnumerable<Company> companies { get; set; }
    }
}