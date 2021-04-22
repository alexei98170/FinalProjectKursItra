using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectKursItra.Models
{
    public enum ELanguage { Ru, En }
    public enum ETheme { Light, Dark }

    public class ApplicationUser : IdentityUser
    {
  
        public bool IsBlocked { get; set; }
    }
}
