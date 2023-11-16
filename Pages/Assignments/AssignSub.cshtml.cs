using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1v3.Pages.Assignments
{
	[Authorize(Policy = "MustBeStudent")]
	public class AssignSubModel : PageModel
	{
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

		public AssignSubModel(Assignment1v3.Data.Assignment1v3Context context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
            _webHostEnvironment = webHostEnvironment;
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
		public async Task<IActionResult> OnPostAsync(IFormFile fileUpload)
		{
			if (fileUpload != null)
			{
				if (fileUpload.Length > 0)
				{
					// Make sure name is unique
					var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileUpload.FileName;

					// Get the physical path to the wwwroot/profilePictures folder
					var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Files");

					// Combine the two
					var filePath = Path.Combine(uploadFolder, uniqueFileName);

					// Save the image in the folder
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await fileUpload.CopyToAsync(stream);
					}
				}
                return Page();
            }
			else {
				return Page();
            }
            
        }
	}
}
	
