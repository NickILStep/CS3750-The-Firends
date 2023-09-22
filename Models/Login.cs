using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
    public class Login
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name_First { get; set; } = string.Empty;

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name_Last { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email_Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
