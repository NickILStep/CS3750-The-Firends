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

            if (Login.ConfirmPassword != Login.Password)
            {
                //add error message "confirm Password didn't match password"
                return Page();
            }
            if (!ModelState.IsValid || _context.Login == null || Login == null)
            {
                return Page();
            }
            // Create a new user record in the database
            var newUser = new Login
            {
                Name_First = Login.Name_First,
                Name_Last = Login.Name_Last,
                Email_Username = Login.Email_Username,
                Password = Login.Password,
                // Set other properties as needed.
            };

            // Add the Login entity to the database
            _context.Login.Add(Login);
            await _context.SaveChangesAsync();

            // Save the new user to the database.
            return RedirectToPage("../Index");
        }

    }
}
