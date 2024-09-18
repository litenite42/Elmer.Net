using System.ComponentModel.DataAnnotations;

namespace Elmer.Net.Sample.Models
{
    public class Course
    {
        public Course()
        {
            Name = "Default course name";
            Description = "Standard Description";
        }
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Capacity { get; set; }
    }
}
