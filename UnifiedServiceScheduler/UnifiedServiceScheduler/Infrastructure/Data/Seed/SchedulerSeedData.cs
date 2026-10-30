using UnifiedServiceScheduler.Domain.Models;
using UnifiedServiceScheduler.Domain.ValueObjects;

namespace UnifiedServiceScheduler.Infrastructure.Data.Seed;

public static class SchedulerSeedData
{
    public static IReadOnlyList<ServiceType> ServiceTypes { get; } =
    [
        new(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Oil Change", "OIL-001", "Oil Service", TimeSpan.FromHours(1)),
        new(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Brake Inspection", "BRK-002", "Brake Service", TimeSpan.FromMinutes(90)),
        new(Guid.Parse("33333333-3333-3333-3333-333333333333"), "Tire Rotation", "TIRE-003", "Tire Service", TimeSpan.FromMinutes(45))
    ];

    public static IReadOnlyList<Bay> Bays { get; } =
    [
        new(Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001"), "Bay 1", "Lift"),
        new(Guid.Parse("bbbbbbbb-0000-0000-0000-000000000001"), "Bay 2", "General"),
        new(Guid.Parse("cccccccc-0000-0000-0000-000000000001"), "Bay 3", "Alignment")
    ];

    public static IReadOnlyList<Technician> Technicians { get; } =
    [
        new(Guid.Parse("11111111-0000-0000-0000-000000000001"), "Alex", new[] { "Oil Service", "Tire Service" }),
        new(Guid.Parse("22222222-0000-0000-0000-000000000001"), "Sam", new[] { "Brake Service", "Tire Service" }),
        new(Guid.Parse("33333333-0000-0000-0000-000000000001"), "Priya", new[] { "Oil Service", "Brake Service" })
    ];

    public static BusinessHours BusinessHours { get; } = new(new TimeOnly(8, 0), new TimeOnly(18, 0));
}
