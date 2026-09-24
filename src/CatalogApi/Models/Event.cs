namespace CatalogApi.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public DateTime DateUtc { get; set; }

    public List<TicketType> TicketTypes { get; set; } = new();
}
