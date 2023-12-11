using Assignment1v3.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1v3.Pages.Payment
{
    public class SuccessfulPaymentModel : PageModel
    {

        
        private readonly Assignment1v3.Data.Assignment1v3Context _context;
        public SuccessfulPaymentModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;            
        }

        [BindProperty]
        public Login Login { get; set; } = default!;
        [BindProperty]
        public string Amount { get; set; }

        public async Task<IActionResult> OnGetAsync(long? amount)
        {
            if (amount == null)
            {
                return NotFound();
            }
            amount = amount / 100;//to get rid of cents
            Amount = "$" + amount.ToString();

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

       
    }
}
