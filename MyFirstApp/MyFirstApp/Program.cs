using Microsoft.Extensions.Primitives;
using MyFirstApp.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CustomMiddleware>();
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run(async (HttpContext context) =>
//{
//    context.Response.StatusCode = 200;
//    await context.Response.WriteAsync("Hello World!");
//    await context.Response.WriteAsync("Ramdayal");

//});
//app.Run(async (HttpContext context) =>
//{
//    string path = context.Request.Path;

//    Console.WriteLine("Requested Path" + path + "  " + context.Request.Method);
//    Console.WriteLine("Query String: " + context.Request.QueryString);
//    if (1 != 1)
//    {
//        context.Response.StatusCode = 200;
//    } else
//    {
//        context.Response.StatusCode = 400;
//    }
//    //string queryName = context.Request.Query.ContainsKey("Name") ? context.Request.Query["Name"]:"Not Found";
//    context.Response.Headers["Context-Type"] = "text/html";
//    await context.Response.WriteAsync("<h1>Hello World!<h1>");
//    if (context.Request.Query.ContainsKey("Name"))
//    {
//        string queryName = context.Request.Query["Name"];
//        await context.Response.WriteAsync($"<h2>{queryName} <h2>");
//    }

//});

//app.Run(async (HttpContext context) =>
//{
//    StreamReader reader = new StreamReader(context.Request.Body);
//    string bodyValue = await reader.ReadToEndAsync();

//    Dictionary<string, StringValues> queryDict  = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(bodyValue);
//  foreach(KeyValuePair<string, StringValues> entry in queryDict)
//    {
//        Console.WriteLine(entry.Key + ":" + entry.Value);
//        await context.Response.WriteAsync($"{entry.Key} : {entry.Value}");
//    }
//});
//new Middleware(app);

//app.UseMiddleware<CustomMiddleware>();
//app.UseMiddleware<CustomMiddleware>();
//app.UseMiddleware<CustomMiddleware>();
//new Middleware(app);

//app.UseCustomMiddleware();
//app.UseCustomMiddleware();
app.UseMyCustomeMiddleware();

/**
 * Right Order of Middleware Execution:
 *  app.UseExceptionHandler("/Error");
 *  app.UseHsts(); // HTTP Strict Transport Security
 *  app.UseHttpsRedirection();
 *  app.UseStaticFiles();
 *  app.UseRouting();
 *  app.Cors();
 *  app.UseAuthentication();
 *  app.Authorization();
 *  app.Use.Session();
 *  app.MapControllers();
 *  // Add the custom middleware here
 * **/

app.UseWhen(
    (context) => context.Request.Path.StartsWithSegments("/custom"), 
 
    appBuilder =>
{
    appBuilder.UseMiddleware<CustomMiddleware>();
});
app.Run();
