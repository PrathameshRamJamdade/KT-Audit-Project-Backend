using AuditManagement.API.Data;
using AuditManagement.API.Models;
using AuditManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuditManagement.API.Repositories.Implementations;

public class ObservationRepository : IObservationRepository
{
    private readonly AuditSystemDbContext _context;

    public ObservationRepository(AuditSystemDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Observation observation)
    {
        await _context.Observations.AddAsync(observation);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Observation>> GetByAuditIdAsync(int auditId)
{
    return await _context.Observations
        .Where(o => o.AuditId == auditId && o.IsDeleted != true)
        .ToListAsync();
}
}