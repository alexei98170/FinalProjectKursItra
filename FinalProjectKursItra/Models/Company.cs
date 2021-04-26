using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.Models
{
    public enum ECategory
    {
        [Display(Name = "Web")]
        Web,
        [Display(Name = "Desktop")]
        Desktop,
        [Display(Name = "Art")]
        Art,
        [Display(Name = "Mobile")]
        Mobile,
        [Display(Name = "Database")]
        Database
    }
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public ECategory Category { get; set; }
        public bool Saved { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy H:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy H:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdate { get; set; }
        public Int64 RatesAmount { get; set; }
        public Int32 RatesCount { get; set; }
    }
}
