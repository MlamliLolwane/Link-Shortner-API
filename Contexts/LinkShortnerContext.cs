using LinkShortnerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortnerAPI.Contexts;

public class LinkShortnerContext : DbContext
{
    public LinkShortnerContext(DbContextOptions<LinkShortnerContext> options) : base(options) { }

    public DbSet<Link> Links { get; set; }
    public DbSet<Click> Clicks { get; set; }

    public DbSet<User> User { get; set; }

    
}


