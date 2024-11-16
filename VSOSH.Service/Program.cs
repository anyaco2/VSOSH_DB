using Microsoft.EntityFrameworkCore;
using VSOSH.Dal;
using VSOSH.Dal.Parser;
using VSOSH.Dal.Repositories;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;
using VSOSH.Service.Api.ParseData;

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

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument();

        builder.Services.AddDbContext<ApplicationDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("Database")));

        builder.Services.AddScoped<IParser, ResultParser>();
        builder.Services.AddScoped<IResultRepository, ResultRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi();
        }

        app.Services.CreateScope()
            .ServiceProvider.GetRequiredService<ApplicationDbContext>()
            .Database.Migrate();

        app.MapParseSchoolOlympiadResultApi();

        app.Run();
    }
}