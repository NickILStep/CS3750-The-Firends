using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Assignment1v3.Pages
{
    [Authorize(Policy = "MustBeStudent")]
    public class NotificationModel : PageModel
    {
        private readonly Data.Assignment1v3Context _context;

        public NotificationModel(Data.Assignment1v3Context context)
        {
            _context = context;
        }
        [BindProperty]
        public Submission Submission { get; set; } = default!;
        [BindProperty]
        public Assignment Assignment { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Submission == null || _context.Assignment == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return Page();
        }
    }
}
