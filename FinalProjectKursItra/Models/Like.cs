using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
    }
}
