using Assignment1v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1v3.Pages.Assignments
{
    public class GradeModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public GradeModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Submission Submission { get; set; }

        public string StudentName { get; set; }
        public string AssignmentName { get; set; }

        public async Task<IActionResult> OnGetAsync(int submissionId)
        {
            Submission = await _context.Submission.FindAsync(submissionId);

            if (Submission == null)
            {
                return NotFound();
            }

            // Get the student name from the Login model associated with the submission
            var student = await _context.Login.FindAsync(Submission.UserID);
            StudentName = $"{student.Name_First} {student.Name_Last}";

            // Get the assignment name from the Assignment model associated with the submission
            var assignment = await _context.Assignment.FindAsync(Submission.AssignmentID);
            AssignmentName = assignment.name;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int submissionID)
        {
            Submission = await _context.Submission
                .Include(s => s.User) // Include related user information
                .FirstOrDefaultAsync(s => s.ID == submissionID);

            if (Submission == null)
            {
                return NotFound();
            }

            if (!int.TryParse(Request.Form["Submission.PointsEarned"], out int pointsEarned))
            {
                ModelState.AddModelError("Submission.PointsEarned", "Invalid points earned value.");
                return Page();
            }

            // Update the PointsEarned property
            Submission.PointsEarned = pointsEarned;

            if (Submission.PointsEarned < 0 || Submission.PointsEarned > Submission.maxPoints)
            {
                ModelState.AddModelError("Submission.PointsEarned", "Points earned must be between 0 and maxPoints.");
                return Page();
            }

            Submission.Graded = true;
            //Submission.modified_date = DateTime.Now;

            _context.Attach(Submission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Submission.Any(e => e.ID == Submission.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./SubmissionList", new { AssignmentID = Submission.AssignmentID.ToString() }); // Redirect to the submission list page after grading
        }
    }
}
