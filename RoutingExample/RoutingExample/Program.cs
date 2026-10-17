using RoutingExample.CustomConstraints;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// How to add the Routing services to the DI container and register custom route constraints in ASP.NET Core 10.0
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthConstaint));
});
var app = builder.Build();

//app.Map("/api/v1/register",( async context =>
//{
//    await context.Response.WriteAsync("Register endpoint");
//}));
app.MapPost("/api/v1/register", async (HttpContext context) =>
{
    var doc = await JsonDocument.ParseAsync(context.Request.Body);

    string username = doc.RootElement.GetProperty("username").GetString()!;
    string password = doc.RootElement.GetProperty("password").GetString()!;

    if(username != null && password != null)
    {
        context.Response.WriteAsJsonAsync(new { username, password });

    }
});


app.Map("files/{filename}.{extension}", async (HttpContext context) =>
{
   await context.Response.WriteAsync($"File requested: {Convert.ToString(context.Request.RouteValues["filename"])}" + " . "+  $"{ Convert.ToString(context.Request.RouteValues["extension"])}");

});

app.Map("users/{id?}",async(HttpContext context) =>
{
    if (context.Request.RouteValues.ContainsKey("id") && context.Request.Query.ContainsKey("name"))
    {
        int id = Convert.ToInt32(context.Request.RouteValues["id"]?.ToString());
        string name = context.Request.Query["name"];
        context.Response.WriteAsync($"User requested with id: {id} and name: {name} ");
    }
    else
    {
        context.Response.WriteAsync("User requested without id");
    }
});

app.Map("daily-digest-report/{reportDate:datetime}", (HttpContext context) =>
{
    DateTime reportDate = Convert.ToDateTime(context.Request.RouteValues["reportDate"]?.ToString());
    context.Response.WriteAsync($"Daily digest report requested for date: {reportDate.ToShortDateString()}");

});

app.Map("cities/{cityid:guid}", (HttpContext context) =>
{
    Guid cityId = Guid.Parse(context.Request.RouteValues["cityid"]?.ToString() ?? Guid.Empty.ToString());
    context.Response.WriteAsync($"City requested with id: {cityId}");
});

app.Map("emplyee/profile/{EmployeeName:minlength(4)=Ram}", async (HttpContext context) =>
{
    string? emplyeeName = context.Request.RouteValues["EmployeeName"]?.ToString();
    await context.Response.WriteAsync($"Employee profile requested for name: {emplyeeName}");
});

app.Map("emplyee/profile/contact/{MobileNumber:length(10)}", async (HttpContext context) =>
{
    string? mobileNumber = context.Request.RouteValues["MobileNumber"]?.ToString();
    await context.Response.WriteAsync($"Employee contact information requested for number: {mobileNumber}");
});

// Eg : Sales-report/2026/JUN
app.Map("sales-report/{year:int:min(1900)}/{month:months}", async (HttpContext context) =>
{
    int year = Convert.ToInt32(context.Request.RouteValues["year"]?.ToString());
    string month = context.Request.RouteValues["month"]?.ToString() ?? string.Empty;
    await context.Response.WriteAsync($"Sales report requested for {month} {year}");
});




app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path} Not Found");
});

app.Run();
