namespace UnifiedServiceScheduler.Domain.Models;

public sealed class Technician
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<string> Skills { get; private set; }
    public bool IsAvailable { get; private set; }

    public Technician(string name, IEnumerable<string> skills, bool isAvailable = true)
        : this(Guid.NewGuid(), name, skills, isAvailable)
    {
    }

    public Technician(Guid id, string name, IEnumerable<string> skills, bool isAvailable = true)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;

        Name = string.IsNullOrWhiteSpace(name)
            ? throw new ArgumentException("Technician name is required.", nameof(name))
            : name.Trim();

        Skills = (skills ?? throw new ArgumentNullException(nameof(skills)))
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (!Skills.Any())
        {
            throw new ArgumentException("At least one technician skill is required.", nameof(skills));
        }

        IsAvailable = isAvailable;
    }

    public bool IsQualifiedFor(ServiceType serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        return Skills.Contains(serviceType.RequiredSkill, StringComparer.OrdinalIgnoreCase);
    }
}
