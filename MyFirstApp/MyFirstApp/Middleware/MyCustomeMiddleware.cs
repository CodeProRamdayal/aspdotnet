using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyFirstApp.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyCustomeMiddleware
    {
        private readonly RequestDelegate _next;

        public MyCustomeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Query.ContainsKey("FirstName") && httpContext.Request.Query.ContainsKey("LastName"))
            {
                string fullName = $"{httpContext.Request.Query["FirstName"]} {httpContext.Request.Query["LastName"]}";
            httpContext.Response.WriteAsync($"Hello, {fullName}!");
            }
            else
            {
                httpContext.Response.WriteAsync("Hello, Guest!");
            }

            await  _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyCustomeMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomeMiddleware>();
        }
    }
}
