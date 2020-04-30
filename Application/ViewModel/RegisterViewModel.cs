using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Potrebno je da unesete ime.")]
        [Display(Name = "Ime")]
        public string name { get; set; }

        [Required(ErrorMessage = "Potrebno je da unesete prezime.")]
        [Display(Name = "Prezime")]
        public string surname { get; set; }
        
        [Required(ErrorMessage = "Potrebno je da unesete email.")]
        [Display(Name = "Email")]
        [EmailAddress]
        [Remote(action: "UsedEmail", controller: "Account")]
        public string email { get; set; }

        [Required(ErrorMessage = "Potrebno je da unesete lozinku.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string password { get; set; }    

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdite lozinku")]
        [Compare("password", ErrorMessage = "Sifre se ne poklapaju")]
        public string repeatPassword { get; set; }

        public string roleName { get; set;}
    }
}