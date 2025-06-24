namespace LinkShortnerAPI.Models;

public class AnonymousLink
{
    public int Id { get; set; }
    public required string Referrer { get; set; }
    public DateTime CreatedAt { get; set; }
}