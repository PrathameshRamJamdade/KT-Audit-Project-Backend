namespace AuditManagement.API.Services.Interfaces;

public interface ICorrectiveActionService
{
    Task CreateActionAsync(CreateCorrectiveActionDto dto);

    Task<List<CorrectiveActionResponseDto>> GetActionsByObservation(int observationId);
}