using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.Models.AccountViewModels
{
    public class LoginViewModel
    {
        
        public string UserName { get; set; }

        [Required(ErrorMessage = "You have entered incorrect data")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
