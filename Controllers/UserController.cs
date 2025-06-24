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
}