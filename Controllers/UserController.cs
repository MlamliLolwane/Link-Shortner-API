using LinkShortnerAPI.Contexts;
using LinkShortnerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkShortnerAPI.Controllers;

[ApiController]
[Route("api/")]
public class UserController(LinkShortnerContext context) : ControllerBase
{
    private readonly LinkShortnerContext _context = context;

    [HttpGet("login/{userName}")]
    public async Task<ActionResult<User>> GetUser(string userName)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == userName);

        if (user == null)
            return NotFound();

        return user;
    }


    [HttpPost("register")]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _context.User.Add(user);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { userName = user.UserName }, user);
    }
    
    [HttpGet("dashboard/{userId}")]
    public async Task<IActionResult> GetUserDashboard(int userId)
    {
        var totalLinks = await _context.Links
            .Where(l => l.UserId == userId)
            .CountAsync();

        var totalClicks = await _context.Clicks
            .Where(c => c.Link.UserId == userId)
            .CountAsync();

        var uniqueReferrers = await _context.Clicks
            .Where(c => c.Link.UserId == userId)
            .Select(c => c.Referrer)
            .Distinct()
            .CountAsync();

        var now = DateTime.UtcNow;
        var sixMonthsAgo = now.AddMonths(-5); // includes current month

        var monthlyClicks = await _context.Clicks
        .Where(c => c.Link.UserId == userId &&
                    c.CreatedAt >= new DateTime(sixMonthsAgo.Year, sixMonthsAgo.Month, 1))
        .GroupBy(c => new { c.CreatedAt.Year, c.CreatedAt.Month })
        .Select(g => new
        {
            Year = g.Key.Year,
            Month = g.Key.Month,
            Count = g.Count()
        })
        .ToListAsync();

        // Fill in missing months with zero clicks
        var monthlyStats = Enumerable.Range(0, 6)
            .Select(i => sixMonthsAgo.AddMonths(i))
            .Select(date =>
        {
            var entry = monthlyClicks.FirstOrDefault(m => m.Year == date.Year && m.Month == date.Month);
            return new
            {
                Month = date.ToString("MMMM"),
                Year = date.Year,
                Count = entry?.Count ?? 0
            };
        })
        .ToList();

        return Ok(new
        {
            TotalLinks = totalLinks,
            TotalClicks = totalClicks,
            UniqueReferrers = uniqueReferrers,
            MonthlyClicks = monthlyStats
        });
    }
}