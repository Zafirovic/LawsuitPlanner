using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public class Contact
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string name { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string phone1 { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string phone2 { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string address { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string email { get; set; }

        public bool legalPerson { get; set; } 

        [Column(TypeName = "varchar(30)")]
        public string job { get; set; }

        public Company company { get; set; }
    }
}