using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public int CommentId { get; set; }
        public string UserId { get; set; }
    }
}
