using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Application.Models
{
    public enum TipSuda
    {
        Osnovni = 0,
        Visi,
        Apelacioni,
        Privredni,
        Prekrsajni,
        Upravni
    }

    public class Lawsuit
    {
        [Key]
        public int id { get; set; }

        [Required]
        public DateTime dateTimeOfEvent { get; set; }

        public Location location { get; set; }

        public Contact judge { get; set; }

        [Required]
        public int courtType { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        public string processId { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string courtroomNumber { get; set; }

        public Contact prosecutor { get; set; }

        public Contact defendant { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string note { get; set; }

        public TypeOfProcess typeOfProcess { get; set; }

        public List<LawsuitLawyer> lawyers { get; set; }
    }
}