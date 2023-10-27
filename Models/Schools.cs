using System.ComponentModel.DataAnnotations;

namespace Assignment1v3.Models
{
    public class Schools
    {
        public List<string> strings = new List<string>();
        public Schools()
        {
            strings.AddRange(new string[]
            {
                "Computer Science",
                "Electric Engineering",
                "Math",
                "Data Science",
                "Pyrotechnics"
            });
        }

    }
}
