using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using VSOSH.Contracts;
using VSOSH.Domain.Services;

namespace VSOSH.Service.Api.MainApi;

/// <summary>
/// Представляет API с основным функционалам.
/// </summary>
public static class MainApi
{
    /// <summary>
    /// Подключает запросы, связанные с основным функционалом.
    /// </summary>
    /// <param name="app"><see cref="WebApplication" />.</param>
    /// <returns><see cref="WebApplication" />.</returns>
    public static WebApplication MapMainApi(this WebApplication app)
    {
        app.MapPassingPointsApi()
            .MapGeneralReportApi()
            .MapQuantitativeDataApi()
            .MapGreaterClassApi();
        return app;
    }

    private static WebApplication MapPassingPointsApi(this WebApplication app)
    {
        app.MapGet("/passingPoints/{subject}", GetPassingPoints)
            .WithOpenApi(operation =>
            {
                operation.Tags = new[] { new OpenApiTag { Name = "MainApi" } };
                operation.Summary = "Формирует проходные баллы по предмету и возвращает excel-файлом.";
                operation.Responses.Add("500",
                    new OpenApiResponse() { Description = "Сформировать баллы не удалось." });
                operation.Responses["200"].Description = "Проходные баллы успешно получены.";

                return operation;
            })
            .DisableAntiforgery();
        return app;
    }

    private static WebApplication MapQuantitativeDataApi(this WebApplication app)
    {
        app.MapGet("/quantitativeData", GetQuantitativeData)
            .WithOpenApi(operation =>
            {
                operation.Tags = new[] { new OpenApiTag { Name = "MainApi" } };
                operation.Summary = "Формирует количественные данные и возвращает excel-файлом.";
                operation.Responses.Add("500",
                    new OpenApiResponse() { Description = "Сформировать данные не удалось." });
                operation.Responses["200"].Description = "Данные успешно получены.";

                return operation;
            })
            .DisableAntiforgery();
        return app;
    }

    private static WebApplication MapGreaterClassApi(this WebApplication app)
    {
        app.MapGet("/greaterClass", GetGreaterClass)
            .WithOpenApi(operation =>
            {
                operation.Tags = new[] { new OpenApiTag { Name = "MainApi" } };
                operation.Summary =
                    "Формирует данные с учениками, которые выступали за более старший класс и возвращает excel-файлом.";
                operation.Responses.Add("500",
                    new OpenApiResponse() { Description = "Сформировать данные не удалось." });
                operation.Responses["200"].Description = "Данные успешно получены.";

                return operation;
            })
            .DisableAntiforgery();
        return app;
    }


    private static WebApplication MapGeneralReportApi(this WebApplication app)
    {
        app.MapGet("/generalReport", GetGeneralReport)
            .WithOpenApi(operation =>
            {
                operation.Tags = new[] { new OpenApiTag { Name = "MainApi" } };
                operation.Summary = "Формирует обший отчет и возвращает excel-файлом.";
                operation.Responses.Add("500",
                    new OpenApiResponse() { Description = "Сформировать отчет не удалось." });
                operation.Responses["200"].Description = "отчет успешно получен.";

                return operation;
            })
            .DisableAntiforgery();
        return app;
    }

    private static async Task<IResult> GetGreaterClass(CancellationToken cancellationToken,
        [FromServices] IGreaterClassService greaterClassService)
    {
        var stream = await greaterClassService.GetGreaterClass(cancellationToken);
        await stream.DisposeAsync();
        var st = new FileStream(stream.Name, FileMode.Open);
        var name = new FileInfo(st.Name).Name;
        return TypedResults.File(st,
            "application/vnd.ms-excel", name);
    }

    private static async Task<IResult> GetQuantitativeData(CancellationToken cancellationToken,
        [FromServices] IQuantitativeDataService quantitativeDataService)
    {
        var stream = await quantitativeDataService.GetQuantitativeData(cancellationToken);

        await stream.DisposeAsync();
        var st = new FileStream(stream.Name, FileMode.Open);
        var name = new FileInfo(st.Name).Name;
        return TypedResults.File(st,
            "application/vnd.ms-excel", name);
    }

    private static async Task<IResult> GetGeneralReport(CancellationToken cancellationToken,
        [FromServices] IGeneralReportService generalReportService)
    {
        var stream = await generalReportService.GetGeneralReport(cancellationToken);
        await stream.DisposeAsync();
        var st = new FileStream(stream.Name, FileMode.Open);
        var name = new FileInfo(st.Name).Name;
        return TypedResults.File(st,
            "application/vnd.ms-excel", name);
    }

    private static async Task<IResult> GetPassingPoints(Subject subject,
        [FromServices] IMapper mapper,
        [FromServices] IPassingPointsService passingPointsService,
        CancellationToken cancellationToken)
    {
        var stream =
            await passingPointsService.GetPassingPoints(mapper.Map<Subject, Domain.Subject>(subject),
                cancellationToken);
        await stream.DisposeAsync();
        var st = new FileStream(stream.Name, FileMode.Open);
        var name = new FileInfo(st.Name).Name;
        return TypedResults.File(st,
            "application/vnd.ms-excel", name);
    }
}