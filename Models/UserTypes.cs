using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
   
        public enum UserType
        {
            [Display(Name = "Instructor")]
            Instructor,
           

            [Display(Name = "Student")]
            Student
            

            
        }
   
}
