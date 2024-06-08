using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pjait_apbd_exam01.Data;
using pjait_apbd_exam01.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace pjait_apbd_exam01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddPayment(PaymentDto paymentDto)
        {
            var client = await _context.Clients.FindAsync(paymentDto.IdClient);
            if (client == null)
            {
                return NotFound("Client not found");
            }

            var subscription = await _context.Subscriptions
                .Include(s => s.Sales)
                .FirstOrDefaultAsync(s => s.IdSubscription == paymentDto.IdSubscription);
            if (subscription == null)
            {
                return NotFound("Subscription not found");
            }

            if (subscription.EndTime < DateTime.Now)
            {
                return BadRequest("Subscription is not active");
            }

            var sale = subscription.Sales.FirstOrDefault(s => s.IdClient == paymentDto.IdClient);
            if (sale == null)
            {
                return BadRequest("Sale not found for this client and subscription");
            }

            var currentPeriodStart = sale.CreatedAt;
            var currentPeriodEnd = sale.CreatedAt.AddMonths(subscription.RenewalPeriod);
            if (sale.Payments.Any(p => p.Date >= currentPeriodStart && p.Date < currentPeriodEnd))
            {
                return BadRequest("Payment for this period already exists");
            }

            var discount = await _context.Discounts
                .Where(d => d.IdSubscription == paymentDto.IdSubscription && d.DateFrom <= DateTime.Now && d.DateTo >= DateTime.Now)
                .OrderByDescending(d => d.Value)
                .FirstOrDefaultAsync();

            var amount = subscription.Price;
            if (discount != null)
            {
                amount -= amount * discount.Value / 100;
            }

            if (paymentDto.Payment != amount)
            {
                return BadRequest("Incorrect payment amount");
            }

            var payment = new Payment
            {
                IdSale = sale.IdSale,
                Amount = paymentDto.Payment,
                Date = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(payment.IdPayment);
        }
    }

    public class PaymentDto
    {
        public int IdClient { get; set; }
        public int IdSubscription { get; set; }
        public decimal Payment { get; set; }
    }
}
