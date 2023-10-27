using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Assignment1v3.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Assignment1v3.Data.Assignment1v3Context _context;
        public ProfileModel(Assignment1v3.Data.Assignment1v3Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Login Login { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //checks userID in cookie
            if (this.User.Claims.ElementAt(3) == null)
            {
                return NotFound();
            }
            //sets login to data from database
            var login = await _context.Login.FirstOrDefaultAsync(m => m.Id == int.Parse(this.User.Claims.ElementAt(3).Value));
            if (login == null)
            {
                return NotFound();
            }
            //binds data
            Login = login;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        
        public async Task<IActionResult> OnPostAsync(IFormFile imageFile)
        {
            var DatabaseLogin = await _context.Login.FirstOrDefaultAsync(m => m.Id == int.Parse(this.User.Claims.ElementAt(3).Value));
            if (DatabaseLogin == null) { return NotFound(); }
            if (imageFile != null)
            {
                if (imageFile.Length > 0)
                {
                    // Make sure name is unique
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                    // Get the physical path to the wwwroot/profilePictures folder
                    var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profilePictures");

                    // Combine the two
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    // Save the image in the folder
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    

                    // Update database variable make sure nothing else changes.
                    Login.ProfilePicturePath = uniqueFileName;
                    Login.Password = DatabaseLogin.Password;
                    Login.ConfirmPassword = DatabaseLogin.ConfirmPassword;
                    Login.Birth_Date = DatabaseLogin.Birth_Date;
                    Login.Role = DatabaseLogin.Role;
                    Login.Email_Username = DatabaseLogin.Email_Username;
                    Login.AddressLine1 = DatabaseLogin.AddressLine1;
                    Login.AddressLine2 = DatabaseLogin.AddressLine2;
                    Login.Bio = DatabaseLogin.Bio;
                    Login.City = DatabaseLogin.City;
                    Login.Name_First = DatabaseLogin.Name_First;
                    Login.Name_Last = DatabaseLogin.Name_Last;
                    Login.PhoneNumber = DatabaseLogin.PhoneNumber;
                    Login.PostalCode = DatabaseLogin.PostalCode;
                    Login.State = DatabaseLogin.State;
                    Login.Link1 = DatabaseLogin.Link1;
                    Login.Link2 = DatabaseLogin.Link2;
                    Login.Link3 = DatabaseLogin.Link3;
                    Login.Id = DatabaseLogin.Id;
                    _context.Entry(DatabaseLogin).State = EntityState.Detached;
                    _context.Attach(Login).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Profile");
                }
            }
            
            
            if (DatabaseLogin.Password == Login.Password) 
            {
                Login.ConfirmPassword = DatabaseLogin.ConfirmPassword;
                Login.Birth_Date = DatabaseLogin.Birth_Date;
                Login.Role = DatabaseLogin.Role;
                Login.Email_Username = DatabaseLogin.Email_Username;//Prevents change to email address
                //All of these are ensuring that if the user leaves a space empty then it 
                Login.ProfilePicturePath = DatabaseLogin.ProfilePicturePath;//can't change image here.
                //keeps the old data in the database if a space is blank
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
                if (Login.State == null)
                {
                    Login.State = DatabaseLogin.State;
                }
                if (Login.Link1 == null)
                {
                    Login.Link1 = DatabaseLogin.Link1;
                }
                if (Login.Link2 == null)
                {
                    Login.Link2 = DatabaseLogin.Link2;
                }
                if (Login.Link3 == null)
                {
                    Login.Link3 = DatabaseLogin.Link3;
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

            return RedirectToPage("./profile");
        }

        
        
        /*public async Task<IActionResult> OnPostAsync(IFormFile imageFile)
        {
            if (imageFile != null)
            {
                if (imageFile.Length > 0)
                {
                    // Make sure name is unique
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                    // Get the physical path to the wwwroot/ProfileImages folder
                    var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProfileImages");

                    // Combine the two
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    // Save the image in the folder
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Update database variable
                    Login.ProfilePicturePath = uniqueFileName;
                }
            }

            // Save database changes
            await _context.SaveChangesAsync();

            return RedirectToPage("./Profile");
        }*/


        private bool LoginExists(int id)
        {
            return (_context.Login?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
