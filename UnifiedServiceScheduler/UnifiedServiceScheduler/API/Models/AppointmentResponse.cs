namespace UnifiedServiceScheduler.API.Models;

public sealed class AppointmentResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ServiceTypeId { get; set; }
    public Guid BayId { get; set; }
    public Guid TechnicianId { get; set; }
    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
