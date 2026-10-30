using UnifiedServiceScheduler.Domain.Models;

namespace UnifiedServiceScheduler.Application.Interfaces;

public interface ISchedulingService
{
    Task<Appointment> BookAppointmentAsync(
        Guid customerId,
        Guid serviceTypeId,
        Guid bayId,
        Guid technicianId,
        DateTime startUtc,
        DateTime endUtc,
        string? notes,
        CancellationToken cancellationToken = default);

    Task CancelAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Appointment>> GetAppointmentsAsync(CancellationToken cancellationToken = default);
    Task<Appointment?> GetAppointmentByIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
}
