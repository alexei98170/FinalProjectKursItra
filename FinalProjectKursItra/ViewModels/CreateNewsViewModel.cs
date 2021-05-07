using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.ViewModels
{
    public class CreateNewsViewModel
    {
        [Required(ErrorMessage = "You have entered incorrect data")]
        public string AuthorId { get; set; }
        [Required(ErrorMessage = "You have entered incorrect data")]
        public string CompanyId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required(ErrorMessage = "You have entered incorrect data")]

        public string Description { get; set; }

        public string Photo { get; set; }

       
    }
}
