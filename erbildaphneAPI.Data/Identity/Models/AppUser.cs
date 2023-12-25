using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace erbildaphneAPI.DataAccess.Identity.Models
{
    public class AppUser : IdentityUser<int>
    {        
        
        public string Name { get; set; }
        
        public string Surname { get; set; }       
        
    }
}
