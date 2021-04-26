using FinalProjectKursItra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.ViewModels
{
    public class CreateCompanyViewModel
    {
        [Required(ErrorMessage = "You have entered incorrect data")]
        public string AuthorId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required(ErrorMessage = "You have entered incorrect data")]

        public string Description { get; set; }
        
        public string Photo { get; set; }
        [Required]
        public string Tags { get; set; }
        public ECategory Category { get; set; }
    }
}
