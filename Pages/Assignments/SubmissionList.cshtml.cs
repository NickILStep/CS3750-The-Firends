using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace Assignment1v3.Pages.Assignments
{
    [Authorize(Policy = "MustBeInstructor")]
    public class SubmissionListModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public SubmissionListModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public IList<Submission> Submissions { get; set; }
        public Course Course { get; set; }
        public Assignment Assignment { get; set; }

        public async Task OnGetAsync(int assignmentId)
        {
            // Retrieve submissions with user information for the specified assignment ID
            Submissions = await _context.Submission
                .Include(s => s.User) // Include the User navigation property
                .Where(s => s.AssignmentID == assignmentId)
                .ToListAsync();

            if (assignmentId != 0) {
                Assignment = await _context.Assignment.FirstOrDefaultAsync(a => a.ID == assignmentId);
                if (Assignment != null) {
                    Course = await _context.Course.FirstOrDefaultAsync(c => c.Id == Assignment.course);
                }
            }
        }
    }
}
