namespace LinkShortnerAPI.Models;

public class Click
{
    public int Id { get; set; }
    public required string OriginalUrl { get; set; }
    public required string ShortenedUrl { get; set; }
    public DateTime CreatedAt { get; set; }

    public int LinkId { get; set; }
    public Link Link { get; set; } = null!;
}