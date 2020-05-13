using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            roles = new List<string>();
        }

        public string Id { get; set; }

        [Required][Display(Name = "Username")]
        public string UserName { get; set; }

        [Required][Display(Name = "Email")]
        [EmailAddress]
        public string email { get; set; }
        
        [Required][Display(Name = "Ime")]
        [StringLength(30)]
        public string name { get; set; }

        [Required][Display(Name = "Prezime")]
        [StringLength(30)]
        public string surname { get; set; }

        [Display(Name = "Nova lozinka")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Lozinka mora da ima najmanje 8 karaktera kao i: veliko slovo (A-Z), malo slovo (a-z), cifru (0-9) i specijalni karakter (e.g. !@#$%^&*)")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdite lozinku")]
        [Compare("password", ErrorMessage = "Sifre se ne poklapaju")]
        public string repeatPassword { get; set; }

        public IList<string> roles { get; set; }
    }
}