using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
    public class Event
    {
        public int id { get; set; }
        public string? title { get; set; }
        [DataType(DataType.Time)]
        public DateTime startTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime endTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime startRecur { get; set; }
        [DataType(DataType.Date)]
        public DateTime endRecur { get; set; }
        public string? daysOfWeek { get; set; }
        public string? url { get; set; }
        public string? userId { get; set; }
        public int courseId { get; set; }
        public int? studSchedId { get; set; }
    }
}
