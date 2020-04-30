using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Potrebno je da unesete email.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Potrebno je da unesete lozinku.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string password { get; set; }  

        [Display(Name = "Zapamti unos")]
        public bool rememberMe { get; set; }
    }
}