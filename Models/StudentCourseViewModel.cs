namespace Assignment1v3.Models
{
    public class StudentCourseViewModel
    {
        public int Id { get; set; } // Add this line to include the Id property

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
    }

}
