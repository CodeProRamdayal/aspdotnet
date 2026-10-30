using System.ComponentModel.DataAnnotations;

namespace UnifiedServiceScheduler.API.Models;

public sealed class CreateAppointmentRequest
{
    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public Guid ServiceTypeId { get; set; }

    [Required]
    public Guid BayId { get; set; }

    [Required]
    public Guid TechnicianId { get; set; }

    [Required]
    public DateTime StartUtc { get; set; }

    [Required]
    public DateTime EndUtc { get; set; }

    public string? Notes { get; set; }
}
