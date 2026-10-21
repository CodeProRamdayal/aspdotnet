using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


Dictionary<string, string> country = new Dictionary<string, string>()
        {
            { "US","United States"},
            { "CN","Canada"},
            {"UK" , "United Kingdom"},
            {"IND","India" },
            {"JP","Japan"}
        };
app.MapGet("/", () => "Hello World!");

app.MapGet("/countries", async (HttpContext context) => { 
 string json = JsonSerializer.Serialize(country);
context.Response.ContentType = "application/json";
await context.Response.WriteAsync(json);
});

app.MapGet("/countries/{countryId:alpha}", async (HttpContext context) => {

    string result = null;
    if (context.Request.RouteValues.ContainsKey("countryId"))
    {
        string countryId = context.Request.RouteValues["countryId"].ToString();
        foreach (var pair in country)
        {
            if (pair.Key == countryId)
            {
                result = JsonSerializer.Serialize(pair);
            }
        }
        if (result !=null)
        {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
        }
        else
        {
          await context.Response.WriteAsync("countryId is not exist!"+ context.Request.RouteValues["countryId"]);
        }
    }
});


app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync("Not Route Found" + context.Request.Path);
});

app.Run();
