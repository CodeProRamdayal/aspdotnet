namespace UnifiedServiceScheduler.Domain.Models;

public sealed class Bay
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string BayType { get; private set; }
    public bool IsActive { get; private set; }

    public Bay(string name, string bayType, bool isActive = true)
        : this(Guid.NewGuid(), name, bayType, isActive)
    {
    }

    public Bay(Guid id, string name, string bayType, bool isActive = true)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;

        Name = string.IsNullOrWhiteSpace(name)
            ? throw new ArgumentException("Bay name is required.", nameof(name))
            : name.Trim();

        BayType = string.IsNullOrWhiteSpace(bayType)
            ? throw new ArgumentException("Bay type is required.", nameof(bayType))
            : bayType.Trim();

        IsActive = isActive;
    }
}
