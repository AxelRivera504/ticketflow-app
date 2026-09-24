using CatalogApi.Data;
using CatalogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly CatalogDbContext _db;

    public EventsController(CatalogDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetAll()
    {
        var events = await _db.Events
            .Include(e => e.TicketTypes)
            .ToListAsync();

        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetById(int id)
    {
        var ev = await _db.Events
            .Include(e => e.TicketTypes)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (ev is null) return NotFound();

        return Ok(ev);
    }

    [HttpPost]
    public async Task<ActionResult<Event>> Create(Event newEvent)
    {
        _db.Events.Add(newEvent);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = newEvent.Id }, newEvent);
    }
}
