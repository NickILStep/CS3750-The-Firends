using Assignment1v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Stripe.Checkout;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1v3.Pages.Payment
{
    public class SuccessModel : PageModel
    {
        public void OnGet()
        {
        }
    }

    namespace server.Controllers
    {
        public class SuccessController : Controller
        {
            private readonly Assignment1v3.Data.Assignment1v3Context _context;


            public SuccessController(Assignment1v3.Data.Assignment1v3Context context)
            {
                // Set your secret key. Remember to switch to your live secret key in production!
                // See your keys here: https://dashboard.stripe.com/apikeys
                StripeConfiguration.ApiKey = "sk_test_51Ny4RlB0mWSJyvtEF6kjAFRYirP53gUXjltsjcI2iMjbQmedAr1zFvqBa9TdFDQ7crej0BI1dEK5r5l21k64Jf1L00G9IYoBmH";
                _context = context;
            }

            [HttpGet("/Payment/Success")]
            public async Task<IActionResult> OrderSuccess([FromQuery] string session_id)
            {
                var sessionService = new Stripe.Checkout.SessionService();
                var Session = sessionService.Get(session_id);
                var SessionPayment = Session.AmountTotal;

                StudentPayments studentPayments = new StudentPayments();
                studentPayments.PaymentAmount = ((double)SessionPayment / 100);
                studentPayments.StudentId = int.Parse(this.User.Claims.ElementAt(3).Value);
                studentPayments.PaymentDate = System.DateTime.Now;
                
                _context.StudentPayments.Add(studentPayments);
                await _context.SaveChangesAsync();

                


                return RedirectToPage("/Payment/SuccessfulPayment", new {amount = SessionPayment});
                
            }
        }
    }
}
