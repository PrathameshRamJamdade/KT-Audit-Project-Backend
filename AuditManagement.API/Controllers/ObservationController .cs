using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuditManagement.API.Services.Interfaces;

[ApiController]
[Route("api/observations")]
[Authorize]
public class ObservationController : ControllerBase
{
    private readonly IObservationService _service;

    public ObservationController(IObservationService service)
    {
        _service = service;
    }

    // Auditor adds observation
    [HttpPost]
    [Authorize(Roles = "Auditor")]
    public async Task<IActionResult> AddObservation([FromBody] CreateObservationDto dto)
    {
        await _service.AddObservationAsync(dto);
        return Ok("Observation added successfully");
    }

    // Admin + Auditor can view
    [HttpGet("{auditId}")]
    [Authorize(Roles = "Admin,Auditor")]
    public async Task<IActionResult> GetObservations(int auditId)
    {
        var result = await _service.GetObservationsByAuditId(auditId);
        return Ok(result);
    }
}