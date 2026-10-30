using UnifiedServiceScheduler.Application.Interfaces;
using UnifiedServiceScheduler.Domain.Exceptions;
using UnifiedServiceScheduler.Domain.Models;
using UnifiedServiceScheduler.Domain.Services;
using UnifiedServiceScheduler.Domain.ValueObjects;
using UnifiedServiceScheduler.Infrastructure.Persistence.InMemory;

namespace UnifiedServiceScheduler.Application.Services;

public sealed class SchedulingService : ISchedulingService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IAppointmentRuleService _ruleService;
    private readonly IReadOnlyCollection<ServiceType> _serviceTypes;
    private readonly IReadOnlyCollection<Bay> _bays;
    private readonly IReadOnlyCollection<Technician> _technicians;
    private readonly BusinessHours _businessHours;

    public SchedulingService(
        IAppointmentRepository appointmentRepository,
        IAppointmentRuleService ruleService,
        IEnumerable<ServiceType> serviceTypes,
        IEnumerable<Bay> bays,
        IEnumerable<Technician> technicians,
        BusinessHours businessHours)
    {
        _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        _ruleService = ruleService ?? throw new ArgumentNullException(nameof(ruleService));
        _serviceTypes = (serviceTypes ?? throw new ArgumentNullException(nameof(serviceTypes))).ToList();
        _bays = (bays ?? throw new ArgumentNullException(nameof(bays))).ToList();
        _technicians = (technicians ?? throw new ArgumentNullException(nameof(technicians))).ToList();
        _businessHours = businessHours ?? throw new ArgumentNullException(nameof(businessHours));
    }

    public async Task<Appointment> BookAppointmentAsync(
        Guid customerId,
        Guid serviceTypeId,
        Guid bayId,
        Guid technicianId,
        DateTime startUtc,
        DateTime endUtc,
        string? notes,
        CancellationToken cancellationToken = default)
    {
        var serviceType = _serviceTypes.FirstOrDefault(s => s.Id == serviceTypeId)
            ?? throw new InvalidOperationException("Selected service type was not found.");

        var bay = _bays.FirstOrDefault(b => b.Id == bayId)
            ?? throw new InvalidOperationException("Selected bay was not found.");

        var technician = _technicians.FirstOrDefault(t => t.Id == technicianId)
            ?? throw new InvalidOperationException("Selected technician was not found.");

        var existingAppointments = await _appointmentRepository.GetAllAsync(cancellationToken);

        try
        {
            _ruleService.ValidateBooking(existingAppointments, serviceType, bay, technician, startUtc, endUtc, _businessHours);
        }
        catch (InvalidOperationException ex)
        {
            throw new AppointmentValidationException(ex.Message);
        }

        var appointment = Appointment.Create(customerId, serviceTypeId, bayId, technicianId, startUtc, endUtc, notes);
        await _appointmentRepository.AddAsync(appointment, cancellationToken);

        return appointment;
    }

    public async Task CancelAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId, cancellationToken)
            ?? throw new InvalidOperationException("Appointment not found.");

        appointment.Cancel();
        await _appointmentRepository.UpdateAsync(appointment, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Appointment>> GetAppointmentsAsync(CancellationToken cancellationToken = default)
    {
        return await _appointmentRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
    {
        return await _appointmentRepository.GetByIdAsync(appointmentId, cancellationToken);
    }
}
