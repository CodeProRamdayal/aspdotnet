using UnifiedServiceScheduler.Domain.Models;

namespace UnifiedServiceScheduler.Infrastructure.Persistence.InMemory;

public interface IAppointmentRepository
{
    Task<IReadOnlyCollection<Appointment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default);
    Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public sealed class InMemoryAppointmentRepository : IAppointmentRepository
{
    private readonly List<Appointment> _appointments = new();

    public Task<IReadOnlyCollection<Appointment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyCollection<Appointment>>(_appointments
            .Where(a => a.Status != AppointmentStatus.Cancelled)
            .ToList());
    }

    public Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_appointments.FirstOrDefault(a => a.Id == id));
    }

    public Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(appointment);
        _appointments.Add(appointment);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(appointment);

        var index = _appointments.FindIndex(a => a.Id == appointment.Id);
        if (index >= 0)
        {
            _appointments[index] = appointment;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == id);
        if (appointment is not null)
        {
            appointment.Cancel();
        }

        return Task.CompletedTask;
    }
}
