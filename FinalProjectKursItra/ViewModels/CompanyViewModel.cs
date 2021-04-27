
using FinalProjectKursItra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectKursItra.Data;

namespace ManualsSite.ViewModels
{
    public class CompanyViewModel
    {
        [Key]
        public Company Company { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
        public ApplicationUser User { get; set; }
        public List<string> Tags { get; set; }   
      
        public ECategory Category { get; set; }
        public ApplicationDbContext Context { get; set; }

        
    }
}
