using LinkShortnerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortnerAPI.Contexts;

public class LinkShortnerContext(DbContextOptions<LinkShortnerContext> options) : DbContext(options)
{
    //public LinkShortnerContext(DbContextOptions<LinkShortnerContext> options) : base(options) { }

    public DbSet<Link> Links { get; set; }
    public DbSet<Click> Clicks { get; set; }

    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Click>()
        .HasOne(c => c.Link)
        .WithMany(l => l.Click)
        .HasForeignKey(c => c.LinkId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}


