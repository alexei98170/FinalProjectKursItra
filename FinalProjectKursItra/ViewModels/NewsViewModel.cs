using FinalProjectKursItra.Data;
using FinalProjectKursItra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.ViewModels
{
    public class NewsViewModel
    {

        [Key]
        public News News { get; set; }
        public ApplicationDbContext Context { get; set; }
        public ApplicationUser User { get; set; }
    }
}
