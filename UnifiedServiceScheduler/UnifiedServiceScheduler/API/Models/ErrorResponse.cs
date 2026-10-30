namespace UnifiedServiceScheduler.API.Models;

public sealed class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
