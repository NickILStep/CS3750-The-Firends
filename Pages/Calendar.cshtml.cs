using Assignment1v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1v3.Pages
{
    public class CalendarModel : PageModel
    {
        private readonly Data.Assignment1v3Context _context;

        public CalendarModel(Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public IList<Event> Event { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Event != null)
            {
                Event = await _context.Event.ToListAsync();
            }
        }
    }


}