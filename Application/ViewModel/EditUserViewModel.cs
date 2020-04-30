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
        public string name { get; set; }

        [Required][Display(Name = "Prezime")]
        public string surname { get; set; }

        public IList<string> roles { get; set; }
    }
}