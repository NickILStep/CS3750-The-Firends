using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
    public class Assignment
    {
        public int ID { get; set; }

        public int course { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int maxPoints { get; set; }

        public DateTime startDate { get; set; }

        public DateTime dueDate { get; set; }

        public string uploadType { get; set; }
      
        public DateTime created_date { get; set; }
    }
}
