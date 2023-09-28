using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment1v3.Pages
{
    public class CalendarModel : PageModel
    {
        private readonly ILogger<CalendarModel> _logger;

        public CalendarModel(ILogger<CalendarModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}