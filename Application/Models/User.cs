using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Application.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string name { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string surname { get; set; }

        public List<LawsuitLawyer> lawsuit { get; set; }
    }
}