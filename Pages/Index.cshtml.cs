using Assignment1v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment1v3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public IndexModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Login Login { get; set; } = default!;



        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Login == null || Login == null)
            {
                return Page();
            }

            var UNameList =  _context.Login.Where(x => x.Email_Username == Login.Email_Username).ToList();
            if (UNameList.Count <= 0) { return NotFound(); }
            else{ 
                var PassList = UNameList.Where(x => x.Password == Login.Password).ToList();
                if (PassList.Count == 1)
                {
                    //SUCCESS
                    //await this.SignIn(UNameList.First());
                    //pass ID through URL possibly or ...
                    return RedirectToPage("/profile", new {id = UNameList[0].Id });
                }
                else return NotFound();
            }
            
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}