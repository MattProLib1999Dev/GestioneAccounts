using Microsoft.AspNetCore.Mvc;
using GestioneAccounts.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GestioneAccounts.BE.Domain.Models;

namespace GestioneAccounts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly ILogger<RoleController> _logger;
    private readonly IWebHostEnvironment _env;

    public RoleController(
        ILogger<RoleController> logger,
        ApplicationDbContext context,
        IMediator mediator,
        IWebHostEnvironment env)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _env = env;
    }

    // POST: api/Role/create
    [HttpPost("create")]
public async Task<IActionResult> CreateRole(RoleCreateDto request)
{
    try
    {
        var newRole = new Role
        {
            Admin = request.Admin,
            User = request.User
        };

        _context.Roles.Add(newRole);
        await _context.SaveChangesAsync();

        return Ok(newRole);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error creating role");
        return StatusCode(500, "Internal server error");
    }
}


    // GET: api/Role/isAdmin?accountId=123
    [HttpGet("isAdmin")]
    public async Task<IActionResult> IsAdmin([FromQuery] long accountId)
    {
        if (accountId <= 0)
            return BadRequest(new { message = "Valid account ID is required." });

        try
        {
            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Id == accountId.ToString());

            if (account == null)
                return NotFound(new { message = "Account not found." });

            if (account.Role != null && account.Role.Admin == "Admin")
                return Ok(new { message = "User is an admin." });

            return Forbid("User is not an admin.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking admin status");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    // GET: api/Role/isUser?accountId=123
    [HttpGet("isUser")]
    public async Task<IActionResult> IsUser([FromQuery] long accountId)
    {
        if (accountId <= 0)
            return BadRequest(new { message = "Valid account ID is required." });

        try
        {
            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Id == accountId.ToString());

            if (account == null)
                return NotFound(new { message = "Account not found." });

            if (account.Role != null && account.Role.User == "User")
                return Ok(new { message = "User is a standard user." });

            return Forbid("User is not a standard user.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking user status");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }
}
