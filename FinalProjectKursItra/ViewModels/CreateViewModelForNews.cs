using FinalProjectKursItra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.ViewModels
{
    public class CreateViewModelForNews
    {
        [Required]
        public string AuthorId { get; set; }
        public News News { get; set; }
    }
}
