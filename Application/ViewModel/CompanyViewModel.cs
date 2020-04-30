using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewModel
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            companies = new List<Company>();
        }
        
        [HiddenInput]
        public int id { get; set;}

        [Required(ErrorMessage="Potrebno je da unesete naziv kompanije")]
        [Display(Name = "Naziv kompanije")]
        public string name { get; set; }

        [Required(ErrorMessage="Potrebno je da unesete adresu kompanije")]
        [Display(Name = "Adresa kompanije")]
        public string address { get; set; }

        public IEnumerable<Company> companies { get; set; }
    }
}