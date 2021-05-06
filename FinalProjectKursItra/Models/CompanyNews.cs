using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.Models
{
    public class CompanyNews
    {
        [Key]
        public int CompanyNewsId { get; set; }
        public int CompanyId { get; set; }
        public int NewsId { get; set; }
    }
}
