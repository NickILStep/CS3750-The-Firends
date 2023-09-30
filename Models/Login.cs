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

        public string Password { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Bio { get; set; }

        [StringLength(100)]
        public string? AddressLine1 { get; set; }

        [StringLength(100)]
        public string? AddressLine2 { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(5)]
        public string? PostalCode { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
