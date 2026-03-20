public class CorrectiveActionResponseDto
{
    public int ActionId { get; set; }

    public int ObservationId { get; set; }

    public int? AssignedToUserId { get; set; }

    public string? ActionDescription { get; set; }

    public DateOnly? DueDate { get; set; }

    public string? Status { get; set; }
}
