using Assignment1v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment1v3.Pages
{
    public class IndexModel : PageModel
    {
        

        public IActionResult OnGet()
        {
            return Page();
        }         
       
    }
}