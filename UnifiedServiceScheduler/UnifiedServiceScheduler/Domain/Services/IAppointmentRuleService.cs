using UnifiedServiceScheduler.Domain.Models;
using UnifiedServiceScheduler.Domain.ValueObjects;

namespace UnifiedServiceScheduler.Domain.Services;

public interface IAppointmentRuleService
{
    void ValidateBooking(
        IEnumerable<Appointment> existingAppointments,
        ServiceType serviceType,
        Bay bay,
        Technician technician,
        DateTime startUtc,
        DateTime endUtc,
        BusinessHours businessHours);
}
