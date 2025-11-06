using System;

namespace OnlineLearningMVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
