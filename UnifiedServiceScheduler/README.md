# Unified Service Scheduler - Run Guide

This project is a modern ASP.NET Core booking service for managing bay and technician appointments.

## 1. Prerequisites

Install the required tools before running the application:

- .NET 10 SDK
- Visual Studio 2026 or VS Code
- PowerShell or a terminal

Verify .NET is installed:

```powershell
dotnet --version
```

Expected result: a .NET 10 version number.

## 2. Open a terminal

Open PowerShell in the project root folder:

```powershell
cd "C:\Users\Ram\aspdotnet\UnifiedServiceScheduler"
```

## 3. Restore dependencies

```powershell
dotnet restore
```

## 4. Build the project

```powershell
dotnet build "UnifiedServiceScheduler\UnifiedServiceScheduler.csproj"
```

If the build succeeds, you will see:

```text
Build succeeded.
```

## 5. Run the application

```powershell
dotnet run --project "UnifiedServiceScheduler\UnifiedServiceScheduler.csproj"
```

When the app starts successfully, you will see output similar to:

```text
Now listening on: http://localhost:5078
Application started. Press Ctrl+C to shut down.
```

## 6. Open the app in the browser

Visit:

- Swagger UI: http://localhost:5078/swagger
- Health endpoint: http://localhost:5078/health

## 7. Test the API

### Health check

```powershell
Invoke-RestMethod -Uri "http://localhost:5078/health"
```

Expected output:

```json
{"status":"Healthy"}
```

### Create an appointment

```powershell
$body = '{
  "customerId": "11111111-1111-1111-1111-111111111111",
  "serviceTypeId": "11111111-1111-1111-1111-111111111111",
  "bayId": "aaaaaaaa-0000-0000-0000-000000000001",
  "technicianId": "11111111-0000-0000-0000-000000000001",
  "startUtc": "2026-01-20T09:00:00Z",
  "endUtc": "2026-01-20T10:00:00Z",
  "notes": "Test booking"
}'

Invoke-RestMethod -Uri "http://localhost:5078/api/appointments" -Method Post -ContentType "application/json" -Body $body
```

## 8. Stop the application

Press:

```text
Ctrl + C
```

in the terminal running the app.

## 9. Visual Studio option

If you prefer Visual Studio:

1. Open `UnifiedServiceScheduler.slnx`
2. Set `UnifiedServiceScheduler` as the startup project
3. Press `F5` to run
4. Browse to the same Swagger URL

## 10. Project structure

- `UnifiedServiceScheduler/Program.cs` - app startup and endpoint registration
- `UnifiedServiceScheduler/Application` - business services
- `UnifiedServiceScheduler/Domain` - domain models and booking rules
- `UnifiedServiceScheduler/Infrastructure` - seed data and in-memory repository
- `UnifiedServiceScheduler/API` - DTOs and API models

## 11. Notes

- This version uses in-memory storage for demonstration and testing.
- The app validates booking rules such as overlap checks, technician qualification, and 15-minute schedule slots.
- Swagger is enabled for testing endpoints in the browser.
