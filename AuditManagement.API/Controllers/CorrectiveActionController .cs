using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuditManagement.API.Services.Interfaces;

[ApiController]
[Route("api/actions")]
[Authorize]
public class CorrectiveActionController : ControllerBase
{
    private readonly ICorrectiveActionService _service;

    public CorrectiveActionController(ICorrectiveActionService service)
    {
        _service = service;
    }

    //  Admin/Auditor assigns action
    [HttpPost]
    [Authorize(Roles = "Admin,Auditor")]
    public async Task<IActionResult> CreateAction([FromBody] CreateCorrectiveActionDto dto)
    {
        await _service.CreateActionAsync(dto);
        return Ok("Corrective action created");
    }

    //  Employee + Admin can view actions
    [HttpGet("{observationId}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> GetActions(int observationId)
    {
        var result = await _service.GetActionsByObservation(observationId);
        return Ok(result);
    }
}