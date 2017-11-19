namespace CameraBazar.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public ICollection<Camera> Cameras { get; set; } = new HashSet<Camera>();
    }
}
