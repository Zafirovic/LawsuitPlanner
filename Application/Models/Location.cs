using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public class Location
    {
        [Key]
        public int id { get; set; }
        
        [Column(TypeName="varchar(40)")]
        public string cityName { get; set; }

    }
}