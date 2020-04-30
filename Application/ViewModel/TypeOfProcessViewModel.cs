using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewModel
{
    public class TypeOfProcessViewModel
    {
        [HiddenInput]
        public int id { get; set;}

        [Required(ErrorMessage="Potrebno je da unesete tip procesa")]
        [Remote(action: "UsedTypeOfProcess", controller: "TypeOfProcess")]
        [Display(Name = "Tip postupka")]
        public string name { get; set; }

        public IEnumerable<TypeOfProcess> processes { get; set; } = new List<TypeOfProcess>();
    }
}