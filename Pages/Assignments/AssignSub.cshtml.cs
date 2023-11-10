using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1v3.Pages.Assignments
{
	[Authorize(Policy = "MustBeStudent")]
	public class AssignSubModel : PageModel
    {

		private readonly Assignment1v3.Data.Assignment1v3Context _context;

		public AssignSubModel(Assignment1v3.Data.Assignment1v3Context context)
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
