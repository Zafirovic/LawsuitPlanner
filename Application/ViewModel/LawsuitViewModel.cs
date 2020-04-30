using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

public class LawsuitViewModel
{
    [HiddenInput]
    public int id { get; set; }

    [Required]
    public DateTime dateTimeOfEvent { get; set; }

    public int location { get; set; }

    public int judge { get; set; }

    [Required]
    public int courtType { get; set; }

    [Required]
    [Column(TypeName = "varchar(15)")]
    public string processId { get; set; }

    [Required]
    [Column(TypeName = "varchar(5)")]
    public string courtroomNumber { get; set; }

    public int prosecutor { get; set; }

    public int defendant { get; set; }

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string note { get; set; }

    public int typeOfProcess { get; set; }

    public IEnumerable<User> lawyers { get; set; } = new List<User>();

    public IEnumerable<Lawsuit> lawsuits { get; set; } = new List<Lawsuit>();

    public IEnumerable<Contact> contacts { get; set; } = new List<Contact>();

    public IEnumerable<Location> locations { get; set; } = new List<Location>();

    public IEnumerable<TypeOfProcess> processes { get; set; } = new List<TypeOfProcess>();

    public Dictionary<int, string> courtTypes { get; set; } = new Dictionary<int, string>();

}