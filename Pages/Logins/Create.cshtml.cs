using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment1v3.Data;
using Assignment1v3.Models;

namespace Assignment1v3.Pages.Logins
{
    public class CreateModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public CreateModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Login Login { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Login == null || Login == null)
            {
                return Page();
            }
            
            // Access the checkbox value through the IsInstructor property
            bool signUpAsInstructor = Login.IsInstructor;

            if (signUpAsInstructor)
            {
                // User has chosen to sign up as an instructor
                // Handle instructor-specific logic here
            }
            else
            {
                // User has not chosen to sign up as an instructor
                // Handle logic for other roles or scenarios
            }
            bool signUpAsIsStudent = Login.IsStudent;

            if (signUpAsIsStudent)
            {
                // User has chosen to sign up as an instructor
                // Handle instructor-specific logic here
            }
            else
            {
                // User has not chosen to sign up as an instructor
                // Handle logic for other roles or scenarios
            }

            // Add the Login entity to the database
            _context.Login.Add(Login);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }

    }
}
