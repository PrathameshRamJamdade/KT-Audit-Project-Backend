using AuditManagement.API.Repositories.Interfaces;
using AuditManagement.API.Services.Interfaces;
using AuditManagement.API.Models;
using Microsoft.Extensions.Logging;

namespace AuditManagement.API.Services.Implementations;

public class CorrectiveActionService : ICorrectiveActionService
{
    private readonly ICorrectiveActionRepository _repository;
    private readonly ILogger<CorrectiveActionService> _logger;

    public CorrectiveActionService(ICorrectiveActionRepository repository,
                                   ILogger<CorrectiveActionService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateActionAsync(CreateCorrectiveActionDto dto)
    {
        _logger.LogInformation("Creating corrective action for ObservationId: {ObservationId}", dto.ObservationId);

        var action = new CorrectiveAction
        {
            ObservationId = dto.ObservationId,
            AssignedToUserId = dto.AssignedToUserId,
            ActionDescription = dto.ActionDescription,
            DueDate = DateOnly.FromDateTime(dto.DueDate),
            Status = dto.Status.ToString()
        };

        await _repository.AddAsync(action);

        _logger.LogInformation("Corrective action created successfully");
    }

    public async Task<List<CorrectiveActionResponseDto>> GetActionsByObservation(int observationId)
    {
        _logger.LogInformation("Fetching actions for ObservationId: {ObservationId}", observationId);

        var actions = await _repository.GetByObservationIdAsync(observationId);

        return actions.Select(a => new CorrectiveActionResponseDto
        {
            ActionId = a.ActionId,
            ObservationId = a.ObservationId,
            AssignedToUserId = a.AssignedToUserId,
            ActionDescription = a.ActionDescription,
            DueDate = a.DueDate,
            Status = a.Status
        }).ToList();
    }
}
