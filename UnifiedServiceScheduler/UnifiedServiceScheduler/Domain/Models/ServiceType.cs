namespace UnifiedServiceScheduler.Domain.Models;

public sealed class ServiceType
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string RequiredSkill { get; private set; }
    public TimeSpan Duration { get; private set; }

    public ServiceType(string name, string code, string requiredSkill, TimeSpan duration)
        : this(Guid.NewGuid(), name, code, requiredSkill, duration)
    {
    }

    public ServiceType(Guid id, string name, string code, string requiredSkill, TimeSpan duration)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;

        Name = string.IsNullOrWhiteSpace(name)
            ? throw new ArgumentException("Service name is required.", nameof(name))
            : name.Trim();

        Code = string.IsNullOrWhiteSpace(code)
            ? throw new ArgumentException("Service code is required.", nameof(code))
            : code.Trim();

        RequiredSkill = string.IsNullOrWhiteSpace(requiredSkill)
            ? throw new ArgumentException("Required skill is required.", nameof(requiredSkill))
            : requiredSkill.Trim();

        if (duration <= TimeSpan.Zero)
        {
            throw new ArgumentException("Service duration must be greater than zero.", nameof(duration));
        }

        Duration = duration;
    }
}
