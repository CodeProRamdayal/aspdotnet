namespace UnifiedServiceScheduler.Domain.ValueObjects;

public sealed record BusinessHours
{
    public TimeOnly OpenTime { get; }
    public TimeOnly CloseTime { get; }

    public BusinessHours(TimeOnly openTime, TimeOnly closeTime)
    {
        if (openTime >= closeTime)
        {
            throw new ArgumentException("Opening time must be earlier than closing time.", nameof(openTime));
        }

        OpenTime = openTime;
        CloseTime = closeTime;
    }

    public bool Contains(DateTime dateTime)
    {
        var time = TimeOnly.FromDateTime(dateTime);
        return time >= OpenTime && time < CloseTime;
    }

    public bool IsWithinTimeWindow(DateTime startUtc, DateTime endUtc)
    {
        var startTime = TimeOnly.FromDateTime(startUtc);
        var endTime = TimeOnly.FromDateTime(endUtc);

        return startTime >= OpenTime && endTime <= CloseTime;
    }
}
