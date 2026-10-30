using System.ComponentModel.DataAnnotations;
using UnifiedServiceScheduler.API.Models;
using UnifiedServiceScheduler.Application.Interfaces;
using UnifiedServiceScheduler.Domain.Exceptions;
using UnifiedServiceScheduler.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSchedulerServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.MapGet("/api/appointments", async (ISchedulingService schedulingService, CancellationToken cancellationToken) =>
{
    var appointments = await schedulingService.GetAppointmentsAsync(cancellationToken);
    return Results.Ok(appointments.Select(a => new AppointmentResponse
    {
        Id = a.Id,
        CustomerId = a.CustomerId,
        ServiceTypeId = a.ServiceTypeId,
        BayId = a.BayId,
        TechnicianId = a.TechnicianId,
        StartUtc = a.StartUtc,
        EndUtc = a.EndUtc,
        Status = a.Status.ToString(),
        Notes = a.Notes
    }));
});

app.MapGet("/api/appointments/{appointmentId:guid}", async (Guid appointmentId, ISchedulingService schedulingService, CancellationToken cancellationToken) =>
{
    var appointment = await schedulingService.GetAppointmentByIdAsync(appointmentId, cancellationToken);

    if (appointment is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new AppointmentResponse
    {
        Id = appointment.Id,
        CustomerId = appointment.CustomerId,
        ServiceTypeId = appointment.ServiceTypeId,
        BayId = appointment.BayId,
        TechnicianId = appointment.TechnicianId,
        StartUtc = appointment.StartUtc,
        EndUtc = appointment.EndUtc,
        Status = appointment.Status.ToString(),
        Notes = appointment.Notes
    });
});

app.MapPost("/api/appointments", async (CreateAppointmentRequest request, ISchedulingService schedulingService, CancellationToken cancellationToken) =>
{
    if (!ValidationHelper.Validate(request, out var errors))
    {
        return Results.ValidationProblem(errors);
    }

    try
    {
        var appointment = await schedulingService.BookAppointmentAsync(
            request.CustomerId,
            request.ServiceTypeId,
            request.BayId,
            request.TechnicianId,
            request.StartUtc,
            request.EndUtc,
            request.Notes,
            cancellationToken);

        return Results.Created($"/api/appointments/{appointment.Id}", new AppointmentResponse
        {
            Id = appointment.Id,
            CustomerId = appointment.CustomerId,
            ServiceTypeId = appointment.ServiceTypeId,
            BayId = appointment.BayId,
            TechnicianId = appointment.TechnicianId,
            StartUtc = appointment.StartUtc,
            EndUtc = appointment.EndUtc,
            Status = appointment.Status.ToString(),
            Notes = appointment.Notes
        });
    }
    catch (AppointmentValidationException ex)
    {
        return Results.Conflict(new ErrorResponse { Message = ex.Message });
    }
    catch (InvalidOperationException ex)
    {
        return Results.Conflict(new ErrorResponse { Message = ex.Message });
    }
});

app.MapDelete("/api/appointments/{appointmentId:guid}", async (Guid appointmentId, ISchedulingService schedulingService, CancellationToken cancellationToken) =>
{
    try
    {
        await schedulingService.CancelAppointmentAsync(appointmentId, cancellationToken);
        return Results.NoContent();
    }
    catch (InvalidOperationException)
    {
        return Results.NotFound();
    }
});

app.Run();

public static class ValidationHelper
{
    public static bool Validate(object model, out Dictionary<string, string[]> errors)
    {
        var validationContext = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, validationContext, results, true);

        errors = results
            .GroupBy(r => r.MemberNames.FirstOrDefault() ?? string.Empty)
            .ToDictionary(g => g.Key, g => g.SelectMany(r => r.ErrorMessage is null ? Array.Empty<string>() : new[] { r.ErrorMessage }).ToArray());

        return isValid;
    }
}

public partial class Program
{
}
