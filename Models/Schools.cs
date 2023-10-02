using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
    public enum Schools
    {
        [Display(Name = "Computer Science")]
        CS,

        [Display(Name = "Electric Engineering")]
        EE,

        [Display(Name = "Math")]
        MATH,

        [Display(Name = "Data Science")]
        DS,

        [Display(Name = "Pyrotechnics")]
        PT
    }
}
