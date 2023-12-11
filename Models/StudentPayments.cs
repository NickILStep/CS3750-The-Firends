using System;

namespace Assignment1v3.Models
{
    public class StudentPayments
    {
        public int Id { get; set; }
        public Double PaymentAmount { get; set; }
        public int StudentId { get; set;}
        public DateTime PaymentDate = DateTime.UtcNow;
    }
}
