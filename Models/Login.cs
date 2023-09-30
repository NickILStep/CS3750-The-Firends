using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
    public class Login
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name_First { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name_Last { get; set; } = string.Empty;

        [Display(Name = "Date of Birth")]
        [BindProperty]
        [DataType(DataType.Date)]
        [MinimumAge(16)] //Set the minimum age to 16 years old
        public DateTime Birth_Date { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email_Username { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)] // This makes the input field a password input
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string Confirm_Password { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Please select a role.")]
        public string Role { get; set; } // This property will hold the selected role ("Student" or "Instructor")





    }
}
