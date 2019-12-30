using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplicationHost.CreateDefaultBuilder(args);
        var app = builder.Build();
        app.Use(async (context, next) => 
        {
            context.Items["Greetings"] = "Hello world";
            context.Items["Path"] = context.Request.Path;
            await next.Invoke();
        });
        
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync($"{context.Items["Greetings"]} for {context.Items["Path"]}");
        });

        await app.RunAsync();
    }
}