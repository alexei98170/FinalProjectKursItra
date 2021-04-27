using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string AuthorId { get; set; }
        public int CompanyId { get; set; }
        public string Content { get; set; }
        public int VoteCount { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy H:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

    }
}
