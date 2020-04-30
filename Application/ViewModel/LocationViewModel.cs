using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewModel
{
    public class LocationViewModel
    {
        public LocationViewModel()
        {
            locations = new List<Location>();
        }

        [HiddenInput]
        public int id { get; set;}

        [Required(ErrorMessage="Potrebno je da unesete naziv grada")]
        [Remote(action: "UsedName", controller: "Location")]
        [Display(Name = "Naziv")]
        public string cityName { get; set; }

        public IEnumerable<Location> locations { get; set; }
    }
}