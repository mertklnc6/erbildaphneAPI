using Microsoft.AspNetCore.Identity;

namespace erbildaphneAPI.DataAccess.Identity.Models
{
    public class AppRole : IdentityRole<int>
    {
        public string Description { get; set; } = string.Empty;
    }
}
