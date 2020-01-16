using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        app.UseStaticFiles();
        app.MapRazorPages();
        app.MapFallbackToPage("/Index");
        app.MapBlazorHub();

        await app.RunAsync();
    }
}
