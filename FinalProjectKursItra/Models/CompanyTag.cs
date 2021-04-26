using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.Models
{
    public class CompanyTag
    {
        [Key]
        public int CompanyTagId { get; set; }
        public int CompanyId { get; set; }
        public int TagId { get; set; }
    }
}
