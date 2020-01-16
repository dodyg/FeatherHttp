﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Carter;

public class HomeModule : CarterModule
{
    public HomeModule()
    {
        Get("/", async (req, res) => await res.WriteAsync("Hello from Carter!"));
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCarter();

        var app = builder.Build();

        app.Listen("http://localhost:3000");

        app.MapCarter();

        await app.RunAsync();
    }
}