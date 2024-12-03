using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using VSOSH.Contracts;
using VSOSH.Dal;
using VSOSH.Dal.Parser;
using VSOSH.Dal.Repositories;
using VSOSH.Dal.Services;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;
using VSOSH.Service.Api.MainApi;
using VSOSH.Service.Api.ParseData;
using VSOSH.Service.Profiles;

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
        if (!Directory.Exists(ProfileLocationStorage.ServiceFiles))
        {
            Directory.CreateDirectory(ProfileLocationStorage.ServiceFiles);
        }

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument();
        builder.Services.ConfigureHttpJsonOptions(op =>
        {
            op.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        builder.Services.AddAutoMapper(typeof(VsoshProfile));

        builder.Services.AddDbContext<ApplicationDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("Database")));

        builder.Services.AddScoped<IParser, ResultParser>();
        builder.Services.AddScoped<IResultRepository, ResultRepository>();
        builder.Services.AddScoped<IPassingPointsService, PassingPointsService>();
        builder.Services.AddScoped<IGeneralReportService, GeneralReportService>();
        builder.Services.AddScoped<IQuantitativeDataService, QuantitativeDataService>();
        builder.Services.AddScoped<IGreaterClassService, GreaterClassService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi();
        }

        app.Services.CreateScope()
            .ServiceProvider.GetRequiredService<ApplicationDbContext>()
            .Database.Migrate();

        app.MapParseSchoolOlympiadResultApi()
            .MapMainApi();

        app.Run();
    }
}