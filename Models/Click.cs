using System.Text.Json.Serialization;

namespace LinkShortnerAPI.Models;

public class Click
{
    public int Id { get; set; }
    public required string Referrer { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public int LinkId { get; set; }
    [JsonIgnore]
    public Link? Link { get; set; }
}