using System;

public class ObservationResponseDto
{
    public int ObservationId { get; set; }

    public int AuditId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Severity { get; set; }
}