namespace BlogSystem.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ICollection<PostComment> Comments { get; set; } = new HashSet<PostComment>();
    }
}
