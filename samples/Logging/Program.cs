using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplicationHost.CreateDefaultBuilder(args);
        builder.Logging.AddConsole();
        builder.Logging.AddFilter((category, level) =>
        {
            return !category.Contains("Microsoft");
        });

        var app = builder.Build();

        app.MapFallback(async (context) =>
        {
            var logger = context.RequestServices.GetService<ILogger<Program>>();

            context.Response.Headers.Add("Content-Type", "text/html");
            await context.Response.WriteAsync("<html><body>");
            await context.Response.WriteAsync($@"
                <h1>Console Logging</h1>
                <italic>Check your console to see the messages generated by the logger.</italic>
                <ul>
                    <li><a href=""/"">Home</a></li>
                    <li><a href=""/about-us"">About Us</a></li>
                    <li><a href=""/contact-us"">Contact Us</a></li>
                    <li><a href=""/catalog"">Catalog</a></li>
                </ul>
            ");
            await context.Response.WriteAsync("</body></html>");
            logger.LogDebug("Request " + context.Request.Path);
        });

        await app.RunAsync();
    }
}
