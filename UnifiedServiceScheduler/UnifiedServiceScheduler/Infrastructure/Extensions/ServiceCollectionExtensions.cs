using UnifiedServiceScheduler.Application.Interfaces;
using UnifiedServiceScheduler.Application.Services;
using UnifiedServiceScheduler.Domain.Services;
using UnifiedServiceScheduler.Domain.ValueObjects;
using UnifiedServiceScheduler.Infrastructure.Data.Seed;
using UnifiedServiceScheduler.Infrastructure.Persistence.InMemory;

namespace UnifiedServiceScheduler.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSchedulerServices(this IServiceCollection services)
    {
        services.AddSingleton<IAppointmentRepository, InMemoryAppointmentRepository>();
        services.AddSingleton<IAppointmentRuleService, AppointmentRuleService>();
        services.AddSingleton(SchedulerSeedData.ServiceTypes);
        services.AddSingleton(SchedulerSeedData.Bays);
        services.AddSingleton(SchedulerSeedData.Technicians);
        services.AddSingleton(SchedulerSeedData.BusinessHours);
        services.AddSingleton<ISchedulingService, SchedulingService>();

        return services;
    }
}
