using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public List<IdentityRole> AllRoles { get; set; }
        [NotMapped]
        [Required]
        public List<string> UserRoles { get; set; }

        public EditUserViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }

    }
}

