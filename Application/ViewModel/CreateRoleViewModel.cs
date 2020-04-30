using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Potrebno je da unesete naziv da biste uneli promene.")]
        [Display(Name = "Naziv nove uloge korisnika u sistemu")]
        public string roleName { get; set; }
    }
}