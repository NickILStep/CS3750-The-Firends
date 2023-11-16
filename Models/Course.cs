using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string School { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; }
        [DataType(DataType.Time)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime? EndTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartRecur { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndRecur { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        [BindProperty]
        public string? ClassDays { get; set; }
        public int  InstructorId { get; internal set; }
    }
}
