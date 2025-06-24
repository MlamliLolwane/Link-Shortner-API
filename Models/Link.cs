using System.Text.Json.Serialization;

namespace LinkShortnerAPI.Models;

public class Link
{
    public int Id { get; set; }
    public required string OriginalUrl { get; set; }
    public string? ShortenedUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }
    public ICollection<Click> Click { get; } = [];
}