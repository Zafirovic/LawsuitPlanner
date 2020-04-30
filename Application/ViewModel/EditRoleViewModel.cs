using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            users = new List<string>();
        }

        [Display(Name = "Id")]
        public string id { get; set; }

        [Required(ErrorMessage = "Naziv uloge je potrebno unesti.")]
        [Display(Name = "Naziv uloge")]
        public string roleName { get; set; }
        public List<string> users { get; set; }
    }
}