using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Web;

using Stripe;
using Stripe.Checkout;

public class StripeOptions
{
    public string option { get; set; }
}

namespace Assignment1v3.Pages.Payment
{

    


    public class PaymentModel : PageModel
    {
        public void OnGet()
        {
            static void Main(string[] args)
            {
                WebHost.CreateDefaultBuilder(args)
                  .UseWebRoot("public")
                  .UseStartup<Startup>()
                  .Build()
                  .Run();
            }
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // This is a public sample test API key.
            // Don’t submit any personally identifiable information in requests made with this key.
            // Sign in to see your own test API key embedded in code samples.
            StripeConfiguration.ApiKey = "pk_test_51Ny4RlB0mWSJyvtEi0l3o5aeaxLe5JyC8ept6tXTuX1CDmJFzlUlutgv6yo3pDlgUVk5Nk3VukRp8hBxdGtP0tY500Aqramtq3";
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }

    [Route("create-checkout-session")]
    [ApiController]
    public class CheckoutApiController : Controller
    {
        


        [HttpPost]
        public ActionResult Create()
        {
            var url = Request.Scheme + "://" + Request.Host.Value;
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
        {
          new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = 346700, // Account balance
              Currency = "usd",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = "Tuition Payment", // First and last name of student + Tuition payment
              },
            },
            Quantity = 1,
          },
        },      
                Mode = "payment",
                SuccessUrl = url + "/Payment/Success",
                CancelUrl = url + "/Payment/Cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        
    }
}
