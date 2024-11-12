using Microsoft.EntityFrameworkCore;
using VSOSH.Dal;

namespace VSOSH.Service;

/// <summary>
/// Класс, запускающий программу.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument();

        builder.Services.AddDbContext<ApplicationDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("Database")));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi();
        }

        app.Services.CreateScope()
            .ServiceProvider.GetRequiredService<ApplicationDbContext>()
            .Database.Migrate();

        app.UseAuthorization();

        app.Run();
    }
}