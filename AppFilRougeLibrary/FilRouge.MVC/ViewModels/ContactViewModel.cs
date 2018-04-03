using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FilRouge.MVC.ViewModels
{
    public class ContactViewModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Display(Name = "Téléphone")]
        public string Tel { get; set; }

        [Required]
        [Display(Name = "Adresse email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Type (Role)")]
        public string Type { get; set; }
    }
}