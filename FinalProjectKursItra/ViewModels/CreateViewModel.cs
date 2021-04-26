using FinalProjectKursItra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string AuthorId { get; set; }
        public Company Company { get; set; }
        public List<string> Tags { get; set; }
      
    }
}
