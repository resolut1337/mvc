using System.ComponentModel.DataAnnotations;

namespace OnlineLearningMVC.Models
{
    public class RegisterViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
