namespace LinkShortnerAPI.Models;

public class Link
{
    public int Id { get; set; }
    public required string Referrer { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Click> Click { get; } = [];
}