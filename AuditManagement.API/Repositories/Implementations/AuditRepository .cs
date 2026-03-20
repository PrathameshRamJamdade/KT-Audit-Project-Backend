using AuditManagement.API.Data;
using AuditManagement.API.Models;
using AuditManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuditManagement.API.Repositories.Implementations;

public class AuditRepository : IAuditRepository
{
    private readonly AuditSystemDbContext _context;

    public AuditRepository(AuditSystemDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Audit audit)
    {
        await _context.Audits.AddAsync(audit);
        await _context.SaveChangesAsync();
    }

    // Admin → audits created by him
    public async Task<List<Audit>> GetAuditsByCreatorAsync(int userId)
    {
        return await _context.Audits
            .Where(a => a.CreatedByUserId == userId && a.IsDeleted != true)
            .ToListAsync();
    }

    // Auditor → audits assigned to him
    public async Task<List<Audit>> GetAuditsByAuditorAsync(int auditorId)
    {
        return await _context.Audits
            .Where(a => a.AuditorId == auditorId && a.IsDeleted != true)
            .ToListAsync();
    }
}