namespace OrdersApi.Models;

public class EventDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<TicketTypeDto> TicketTypes { get; set; } = new();
}

public class TicketTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }
}
