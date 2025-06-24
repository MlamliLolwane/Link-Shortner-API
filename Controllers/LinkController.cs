using LinkShortnerAPI.Contexts;
using LinkShortnerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkShortnerAPI.Controllers;

[ApiController]
[Route("api/link/")]
public class LinkController(LinkShortnerContext context) : ControllerBase
{
    private readonly LinkShortnerContext _context = context;

    [HttpPost]
    public async Task<ActionResult<Link>> CreateLink(Link link)
    {
        var shortCode = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
        .Replace("/", "_")   // Make it URL-safe
        .Replace("+", "-")
        .Substring(0, 8);

        link.ShortenedUrl = $"http://localhost:5080/{shortCode}";
        link.CreatedAt = DateTime.UtcNow;

        _context.Links.Add(link);
        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("link-details/user/{userId}")] //authenticated/links
    public async Task<IActionResult> GetAllUserLinksWithClickCounts(int userId)
    {
        var links = await _context.Links
            .Where(l => l.UserId == userId)
            .Select(l => new
            {
                LinkId = l.Id,
                l.OriginalUrl,
                l.ShortenedUrl,
                l.CreatedAt,
                ClickCount = l.Click.Count
            })
            .ToListAsync();

        return Ok(links);
    }

    [HttpGet("referrer-stats/user/{userId}/link/{linkId}")] //authenticated/links/view
    public async Task<IActionResult> GetReferrerStatsForLink(int userId, int linkId)
    {
        // Validate that the link belongs to the user
        var link = await _context.Links
            .Where(l => l.Id == linkId && l.UserId == userId)
            .FirstOrDefaultAsync();

        if (link == null)
            return NotFound(new { message = "Link not found for this user." });

        // Group clicks by referrer for the specified link
        var referrerStats = await _context.Clicks
            .Where(c => c.LinkId == linkId)
            .GroupBy(c => c.Referrer)
            .Select(g => new
            {
                Referrer = g.Key,
                ClickCount = g.Count()
            })
            .OrderByDescending(r => r.ClickCount)
            .ToListAsync();

        // Return result with Link metadata
        return Ok(new
        {
            LinkId = link.Id,
            link.OriginalUrl,
            link.ShortenedUrl,
            link.CreatedAt,
            Referrers = referrerStats
        });
    }
}