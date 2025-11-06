using Microsoft.AspNetCore.Identity;

namespace OnlineLearningMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
