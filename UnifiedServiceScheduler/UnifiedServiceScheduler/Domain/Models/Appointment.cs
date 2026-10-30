namespace UnifiedServiceScheduler.Domain.Models;

public enum AppointmentStatus
{
    Scheduled,
    Cancelled,
    Completed
}

public sealed class Appointment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CustomerId { get; private set; }
    public Guid ServiceTypeId { get; private set; }
    public Guid BayId { get; private set; }
    public Guid TechnicianId { get; private set; }
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public string? Notes { get; private set; }
    public AppointmentStatus Status { get; private set; }

    private Appointment()
    {
    }

    public static Appointment Create(
        Guid customerId,
        Guid serviceTypeId,
        Guid bayId,
        Guid technicianId,
        DateTime startUtc,
        DateTime endUtc,
        string? notes = null)
    {
        if (customerId == Guid.Empty) throw new ArgumentException("Customer id is required.", nameof(customerId));
        if (serviceTypeId == Guid.Empty) throw new ArgumentException("Service type id is required.", nameof(serviceTypeId));
        if (bayId == Guid.Empty) throw new ArgumentException("Bay id is required.", nameof(bayId));
        if (technicianId == Guid.Empty) throw new ArgumentException("Technician id is required.", nameof(technicianId));
        if (startUtc == default) throw new ArgumentException("Start time is required.", nameof(startUtc));
        if (endUtc <= startUtc) throw new ArgumentException("End time must be greater than start time.", nameof(endUtc));

        var appointment = new Appointment
        {
            CustomerId = customerId,
            ServiceTypeId = serviceTypeId,
            BayId = bayId,
            TechnicianId = technicianId,
            StartUtc = startUtc,
            EndUtc = endUtc,
            Notes = notes,
            Status = AppointmentStatus.Scheduled
        };

        return appointment;
    }

    public void Cancel()
    {
        if (Status == AppointmentStatus.Cancelled)
        {
            return;
        }

        Status = AppointmentStatus.Cancelled;
    }

    public bool ConflictsWith(DateTime startUtc, DateTime endUtc)
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            return false;
        }

        return StartUtc < endUtc && startUtc < EndUtc;
    }
}
