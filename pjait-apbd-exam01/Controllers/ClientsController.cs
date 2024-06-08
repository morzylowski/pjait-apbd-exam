using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pjait_apbd_exam01.Data;
using pjait_apbd_exam01.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace pjait_apbd_exam01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Sales)
                .ThenInclude(s => s.Subscription)
                .Include(c => c.Sales)
                .ThenInclude(s => s.Payments)
                .FirstOrDefaultAsync(c => c.IdClient == id);

            if (client == null)
            {
                return NotFound();
            }

            var clientDto = new ClientDto
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                Subscriptions = client.Sales.Select(s => new SubscriptionDto
                {
                    IdSubscription = s.Subscription.IdSubscription,
                    Name = s.Subscription.Name,
                    TotalPaidAmount = s.Payments.Sum(p => p.Amount)
                }).ToList()
            };

            return Ok(clientDto);
        }
    }

    public class ClientDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public List<SubscriptionDto>? Subscriptions { get; set; }
    }

    public class SubscriptionDto
    {
        public int IdSubscription { get; set; }
        public string? Name { get; set; }
        public decimal TotalPaidAmount { get; set; }
    }
}
