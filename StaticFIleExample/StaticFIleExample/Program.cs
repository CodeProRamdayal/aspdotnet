using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(
// if you are make the own folder for keeping the static file instited of useing default wwwroot folder the
// You need to configure the file in application builder 
    new WebApplicationOptions()
    {
        WebRootPath ="myroot"
    });

// If You have multiple folder where you take static file from multiple folders
var app = builder.Build();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "mywebroot"))
});
app.MapGet("/", () => "Hello World!");

app.Run();
