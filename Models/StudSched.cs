using System.ComponentModel.DataAnnotations;
namespace Assignment1v3.Models
{
    public class StudSched
    {
        public int Id { get; set; } //Primary key
        
        public string Email_Username { get; set; }

        public int CourseNum { get; set; }
        public int StudId { get; set; }
        public int? CourseId {  get; set; }

      //  public UserCourse UserCourse { get; set; }

    }
}
