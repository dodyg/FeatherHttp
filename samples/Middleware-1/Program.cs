using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var app = builder.Build();
        app.Use(async (context, next) => 
        {
            context.Items["Greetings"] = "Hello world";
            context.Items["Path"] = context.Request.Path;
            context.Items["Endpoint"] = context.GetEndpoint().ToString();
            await next.Invoke();
        });
        
        app.MapFallback(async (context) =>
        {
            await context.Response.WriteAsync($"{context.Items["Greetings"]} for path {context.Items["Path"]} at endpoint { context.Items["Endpoint"] }");
        });

        await app.RunAsync();
    }
}
