using System.Text.Json.Serialization;

namespace CatalogApi.Models;

public class TicketType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }

    public int EventId { get; set; }

    [JsonIgnore]
    public Event? Event { get; set; }
}