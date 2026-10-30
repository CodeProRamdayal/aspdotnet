namespace UnifiedServiceScheduler.Domain.Exceptions;

public sealed class AppointmentValidationException : InvalidOperationException
{
    public AppointmentValidationException(string message)
        : base(message)
    {
    }
}
