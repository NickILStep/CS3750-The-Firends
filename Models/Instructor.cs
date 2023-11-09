namespace Assignment1v3.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Email_Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<InstructorCourse> Courses { get; set; }
    }
}
