using System.ComponentModel.DataAnnotations;

namespace OnlineLearningMVC.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Треба ввести назву курсу")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
