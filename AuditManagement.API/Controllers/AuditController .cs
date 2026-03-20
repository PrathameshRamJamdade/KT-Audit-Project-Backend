using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuditManagement.API.Services.Interfaces;

[ApiController]
[Route("api/audits")]
[Authorize]
public class AuditController : ControllerBase
{
    private readonly IAuditService _auditService;

    public AuditController(IAuditService auditService)
    {
        _auditService = auditService;
    }

    // Only Admin creates audit
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAudit([FromBody] CreateAuditDto dto)
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);

        await _auditService.CreateAuditAsync(dto, userId);
        return Ok("Audit created successfully");
    }

    // Admin → own audits
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAuditsForAdmin()
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);

        var result = await _auditService.GetAuditsForAdmin(userId);
        return Ok(result);
    }

    // Auditor → assigned audits
    [HttpGet("auditor")]
    [Authorize(Roles = "Auditor")]
    public async Task<IActionResult> GetAuditsForAuditor()
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);

        var result = await _auditService.GetAuditsForAuditor(userId);
        return Ok(result);
    }
}