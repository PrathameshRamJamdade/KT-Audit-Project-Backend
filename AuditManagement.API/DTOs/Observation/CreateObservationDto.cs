using System;

public class CreateObservationDto
{
    public int AuditId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Severity Severity { get; set; }

    public DateTime DueDate { get; set; }
}