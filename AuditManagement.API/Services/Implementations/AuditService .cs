using AuditManagement.API.Repositories.Interfaces;
using AuditManagement.API.Services.Interfaces;
using AuditManagement.API.Models;
using Microsoft.Extensions.Logging;

namespace AuditManagement.API.Services.Implementations;

public class AuditService : IAuditService
{
    private readonly IAuditRepository _auditRepository;
    private readonly ILogger<AuditService> _logger;

    public AuditService(IAuditRepository auditRepository, ILogger<AuditService> logger)
    {
        _auditRepository = auditRepository;
        _logger = logger;
    }

    public async Task CreateAuditAsync(CreateAuditDto dto, int createdByUserId)
    {
        _logger.LogInformation("Creating audit: {AuditName}", dto.AuditName);

        var audit = new Audit
        {
            AuditName = dto.AuditName,
            DepartmentId = dto.DepartmentId,
            AuditorId = dto.AuditorId,
            StartDate = DateOnly.FromDateTime(dto.StartDate),
            EndDate = DateOnly.FromDateTime(dto.EndDate),
            Status = dto.Status.ToString(),
            CreatedByUserId = createdByUserId
        };

        await _auditRepository.AddAsync(audit);

        _logger.LogInformation("Audit created successfully: {AuditName}", dto.AuditName);
    }

    public async Task<List<AuditResponseDto>> GetAuditsForAdmin(int adminId)
    {
        _logger.LogInformation("Fetching audits for Admin: {AdminId}", adminId);

        var audits = await _auditRepository.GetAuditsByCreatorAsync(adminId);

        return audits.Select(a => new AuditResponseDto
        {
            AuditId = a.AuditId,
            AuditName = a.AuditName,
            Status = a.Status,
            StartDate = a.StartDate,
            EndDate = a.EndDate
        }).ToList();
    }

    public async Task<List<AuditResponseDto>> GetAuditsForAuditor(int auditorId)
    {
        _logger.LogInformation("Fetching audits for Auditor: {AuditorId}", auditorId);

        var audits = await _auditRepository.GetAuditsByAuditorAsync(auditorId);

        return audits.Select(a => new AuditResponseDto
        {
            AuditId = a.AuditId,
            AuditName = a.AuditName,
            Status = a.Status,
            StartDate = a.StartDate,
            EndDate = a.EndDate
        }).ToList();
    }
}
