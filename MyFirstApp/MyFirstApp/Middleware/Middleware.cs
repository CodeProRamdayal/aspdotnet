namespace MyFirstApp.Middleware
{
    public class Middleware
    {
        public Middleware(WebApplication webApp)
        {
            // MiddleWare 1 forward the request to next middleware in the pipeline
            // Same way can do multiple middlewares in the pipeline and can forward the request to next middleware in the pipeline
            webApp.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("Hello");
                await next(context);
            });

            // MiddleWare 2  when use Run method then it will not forward the request to next middleware in the pipeline
            webApp.Run(async (HttpContext context) =>
            {
                await context.Response.WriteAsync("Hello Again");
            });
        }
    }
}
