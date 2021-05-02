using FinalProjectKursItra.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinalProjectKursItra.Data;

namespace FinalProjectKursItra.ViewModels
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
