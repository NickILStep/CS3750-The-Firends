using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;

namespace Assignment1v3.Pages.Assignments
{
    public class DetailsModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public DetailsModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

      public Assignment Assignment { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Assignment == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment.FirstOrDefaultAsync(m => m.ID == id);
            if (assignment == null)
            {
                return NotFound();
            }
            else 
            {
                Assignment = assignment;
            }
            return Page();
        }
    }
}
