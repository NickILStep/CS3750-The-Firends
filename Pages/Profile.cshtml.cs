using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Assignment1v3.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;
        public ProfileModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Login Login { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (this.User.Claims.ElementAt(3) == null)
            {
                return NotFound();
            }

            var login = await _context.Login.FirstOrDefaultAsync(m => m.Id == int.Parse(this.User.Claims.ElementAt(3).Value));
            if (login == null)
            {
                return NotFound();
            }
            Login = login;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var DatabaseLogin = await _context.Login.FirstOrDefaultAsync(m => m.Id == int.Parse(this.User.Claims.ElementAt(3).Value));
            
            if (DatabaseLogin.Password == Login.Password) 
            {
                Login.ConfirmPassword = DatabaseLogin.ConfirmPassword;
                Login.Birth_Date = DatabaseLogin.Birth_Date;
                Login.Role = DatabaseLogin.Role;
                Login.Email_Username = DatabaseLogin.Email_Username;//Prevents change to email address
                if(Login.AddressLine1 == null)
                {
                    Login.AddressLine1 = DatabaseLogin.AddressLine1;
                }
                if (Login.AddressLine2 == null)
                {
                    Login.AddressLine2 = DatabaseLogin.AddressLine2;
                }
                if (Login.Bio == null)
                {
                    Login.Bio = DatabaseLogin.Bio;
                }
                if (Login.City == null)
                {
                    Login.City = DatabaseLogin.City;
                }
                if (Login.Name_First == null)
                {
                    Login.Name_First = DatabaseLogin.Name_First;
                }
                if (Login.Name_Last == null) 
                { 
                    Login.Name_Last = DatabaseLogin.Name_Last; 
                }
                if (Login.PhoneNumber == null)
                {
                    Login.PhoneNumber = DatabaseLogin.PhoneNumber;
                }
                if (Login.PostalCode == null)
                {
                    Login.PostalCode = DatabaseLogin.PostalCode;
                }
                if (Login.Role == null)
                {
                    Login.Role = DatabaseLogin.Role;
                }
                if (Login.State == null)
                {
                    Login.State = DatabaseLogin.State;
                }
                _context.Entry(DatabaseLogin).State = EntityState.Detached;
                _context.Attach(Login).State = EntityState.Modified;
            }else 
            {
                ModelState.AddModelError("Login.Password", "Incorrect Password, Please Try Again.");
                return Page();
            }


            try
            {
                //this.Login.ConfirmPassword = _context.
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(Login.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./profile", new { id = Login.Id });
        }

        private bool LoginExists(int id)
        {
            return (_context.Login?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
