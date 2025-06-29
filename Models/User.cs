namespace LinkShortnerAPI.Models;

public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public ICollection<Link> Link { get; } = [];
}