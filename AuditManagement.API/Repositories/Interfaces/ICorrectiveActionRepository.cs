using AuditManagement.API.Models;

namespace AuditManagement.API.Repositories.Interfaces;

public interface ICorrectiveActionRepository
{
    Task AddAsync(CorrectiveAction action);

    Task<List<CorrectiveAction>> GetByObservationIdAsync(int observationId);
}