using Microsoft.AspNetCore.Identity;

namespace YourAnimeList.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation property for the many-to-many relationship
        public List<UserAnimeList> UserAnimeLists { get; set; }
    }
}
