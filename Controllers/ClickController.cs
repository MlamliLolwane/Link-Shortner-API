using LinkShortnerAPI.Contexts;
using LinkShortnerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkShortnerAPI.Controllers;

[ApiController]
public class ClickController(LinkShortnerContext context) : ControllerBase
{
    private readonly LinkShortnerContext _context = context;

    [HttpGet("api/clicks/referrers/user/{userId}")] //authenticated/referrers
    public async Task<IActionResult> GetAllReferrersOrdered(int userId)
    {
        var referrers = await _context.Clicks
            .Where(c => c.Link.UserId == userId)
            .GroupBy(c => c.Referrer)
            .Select(g => new
            {
                Referrer = g.Key,
                ClickCount = g.Count()
            })
            .OrderByDescending(r => r.ClickCount)
            .ToListAsync();

        return Ok(referrers);
    }

    [HttpGet("{shortCode}")]
    public async Task<IActionResult> RedirectFromShortenedUrl(string shortCode)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}/";
        var fullShortenedUrl = $"{baseUrl}{shortCode}";

        // Look up the link
        var link = await _context.Links
            .FirstOrDefaultAsync(l => l.ShortenedUrl == fullShortenedUrl);

        if (link == null)
            return NotFound("Shortened URL not found.");

        // Create a new click
        var click = new Click
        {
            LinkId = link.Id,
            Referrer = "Other" // default fallback
        };

        // Try to extract the referer domain
        if (Request.Headers.TryGetValue("Referer", out var refererHeader))
        {
            if (Uri.TryCreate(refererHeader.ToString(), UriKind.Absolute, out var uri))
            {
                click.Referrer = $"{uri.Scheme}://{uri.Host}";
            }
        }

        _context.Clicks.Add(click);
        await _context.SaveChangesAsync();

        // Redirect to the original URL
        return RedirectPermanent(link.OriginalUrl);
    }
}