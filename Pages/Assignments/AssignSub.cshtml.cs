using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Assignment1v3.Pages.Assignments
{
	[Authorize(Policy = "MustBeStudent")]
	public class AssignSubModel : PageModel
	{
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Assignment1v3.Data.Assignment1v3Context _context;
		public int userID;
		public string uniquefilename;
        public int assignid;
        public bool unitTesting = false;
		public AssignSubModel(Assignment1v3.Data.Assignment1v3Context context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Submission Submission { get; set; } = default!;
		[BindProperty]
        public Assignment Assignment { get; set; } = default!;

        
		public async Task<IActionResult> OnGetAsync(int id)
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
            if(!unitTesting)
            {
                var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                System.Diagnostics.Debug.WriteLine(userEmailClaim);
            
                if (userEmailClaim != null)
                {
                    var userEmailClaimValue = userEmailClaim.Value;

                    // Find the user by email address
                    var user = await _context.StudSched
                        .FirstOrDefaultAsync(x => x.Email_Username.Contains(userEmailClaimValue));
                    System.Diagnostics.Debug.WriteLine(user);

                    if (user != null)
                    {
                        var userId = user.StudId;
                        System.Diagnostics.Debug.WriteLine(userId);

                        userID = userId;


                    }
                }
            }

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
					uniquefilename = uniqueFileName;

                    
                    

                    

                }
                Submission.Upload = uniquefilename;
                Submission.TextBox = Submission.TextBox;
                Submission.UserID = userID;
                Submission.AssignmentID = Assignment.ID;
                Submission.submissionType = "File Upload";
                Submission.modified_date = DateTime.Now;
                


                _context.Submission.Add(Submission);
            }
			else {
                Submission.Upload = uniquefilename;
                Submission.TextBox = Submission.TextBox;
                Submission.UserID = userID;
                Submission.AssignmentID = Assignment.ID;
                Submission.submissionType = "Text Box";
                Submission.modified_date = DateTime.Now;


                _context.Submission.Add(Submission);

                
                
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./StudentCourseViewDetails", new { id = Submission.AssignmentID.ToString()}); 

        }
	}
}
	
