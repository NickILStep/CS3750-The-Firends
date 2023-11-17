using System.ComponentModel.DataAnnotations;
namespace Assignment1v3.Models
{
    public class Submission
    {
        public int ID { get; set; }

        public int AssignmentID { get; set; }

        public int UserID { get; set; }
        public int maxPoints { get; set; }

        public int PointsEarned { get; set; }

        public string submissionType { get; set; }

        public string? Upload { get; set;} = string.Empty;
        public string? TextBox { get; set; }
        public bool? Graded { get; set; } = false;

    }
}