using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Application.Models
{
    public class LawsuitLawyer
    {
        [Key]
        public int id { get; set; }

        public int lawsuitId { get; set; }
        public Lawsuit lawsuit { get; set; }

        public string userId { get; set; }
        public User user { get; set; }
    }
}