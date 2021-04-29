using FinalProjectKursItra.Data;
using FinalProjectKursItra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.ViewModels
{
    public class HomeIndexViewModel
    {
        public int? TagId { get; set; }
        public List<Company> Companies { get; set; }
        public List<Tag> Tags { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationDbContext Context { get; set; }
    }
}
