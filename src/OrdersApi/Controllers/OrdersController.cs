using OrdersApi.Data;
using OrdersApi.Models;
using OrdersApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrdersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrdersDbContext _db;
    private readonly CatalogApiClient _catalogApi;

    public OrdersController(OrdersDbContext db, CatalogApiClient catalogApi)
    {
        _db = db;
        _catalogApi = catalogApi;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAll()
    {
        var orders = await _db.Orders.ToListAsync();
        return Ok(orders);
    }

    public class CreateOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public int EventId { get; set; }
        public int TicketTypeId { get; set; }
        public int Quantity { get; set; }
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Create(CreateOrderRequest request)
    {
        var ev = await _catalogApi.GetEventAsync(request.EventId);

        if (ev is null)
            return BadRequest($"El evento {request.EventId} no existe en CatalogApi.");

        var ticketType = ev.TicketTypes.FirstOrDefault(t => t.Id == request.TicketTypeId);

        if (ticketType is null)
            return BadRequest($"El tipo de ticket {request.TicketTypeId} no existe para ese evento.");

        if (ticketType.QuantityAvailable < request.Quantity)
            return BadRequest("No hay suficientes tickets disponibles.");

        var order = new Order
        {
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            EventId = ev.Id,
            TicketTypeId = ticketType.Id,
            TicketTypeName = ticketType.Name,
            Quantity = request.Quantity,
            UnitPrice = ticketType.Price
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), order);
    }
}
