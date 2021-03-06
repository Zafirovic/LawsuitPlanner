using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public class Company
    {
        [Key]
        public int id { get; set; } 

        [Column(TypeName = "varchar(40)")]
        public string name { get; set; }  
        
        [Column(TypeName = "varchar(40)")] 
        public string address { get; set; }  
    }
}