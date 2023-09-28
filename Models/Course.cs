using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
