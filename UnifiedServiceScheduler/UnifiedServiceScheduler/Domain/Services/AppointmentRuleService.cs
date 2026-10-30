using UnifiedServiceScheduler.Domain.Exceptions;
using UnifiedServiceScheduler.Domain.Models;
using UnifiedServiceScheduler.Domain.ValueObjects;

namespace UnifiedServiceScheduler.Domain.Services;

public sealed class AppointmentRuleService : IAppointmentRuleService
{
    public void ValidateBooking(
        IEnumerable<Appointment> existingAppointments,
        ServiceType serviceType,
        Bay bay,
        Technician technician,
        DateTime startUtc,
        DateTime endUtc,
        BusinessHours businessHours)
    {
        ArgumentNullException.ThrowIfNull(existingAppointments);
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(bay);
        ArgumentNullException.ThrowIfNull(technician);
        ArgumentNullException.ThrowIfNull(businessHours);

        if (!businessHours.IsWithinTimeWindow(startUtc, endUtc))
        {
            throw new AppointmentValidationException("Appointment must fall within business hours.");
        }

        if (startUtc.Minute % 15 != 0 || endUtc.Minute % 15 != 0)
        {
            throw new AppointmentValidationException("Appointments must be scheduled in 15-minute increments.");
        }

        if (startUtc.Date != endUtc.Date)
        {
            throw new AppointmentValidationException("Appointment cannot span multiple days.");
        }

        if (endUtc - startUtc != serviceType.Duration)
        {
            throw new AppointmentValidationException("Appointment duration must match the selected service duration.");
        }

        if (!technician.IsQualifiedFor(serviceType))
        {
            throw new AppointmentValidationException("The selected technician is not qualified for this service.");
        }

        if (!bay.IsActive)
        {
            throw new AppointmentValidationException("The selected bay is not active.");
        }

        var overlaps = existingAppointments
            .Where(a => a.Status == AppointmentStatus.Scheduled)
            .Any(a =>
                (a.BayId == bay.Id || a.TechnicianId == technician.Id) &&
                a.StartUtc < endUtc &&
                startUtc < a.EndUtc);

        if (overlaps)
        {
            throw new AppointmentValidationException("The bay or technician is already booked for the selected time.");
        }
    }
}
