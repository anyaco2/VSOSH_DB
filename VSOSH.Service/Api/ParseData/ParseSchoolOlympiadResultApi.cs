using Microsoft.OpenApi.Models;
using VSOSH.Domain.Services;

namespace VSOSH.Service.Api.ParseData;

/// <summary>
/// Представляет API для парсинга результатов по олимпиаде.
/// </summary>
public static class ParseSchoolOlympiadResultApi
{
    /// <summary>
    /// Подключает запросы, связанные с парсингом результатов по олимпиаде.
    /// </summary>
    /// <param name="app"><see cref="WebApplication" />.</param>
    /// <returns><see cref="WebApplication" />.</returns>
    public static WebApplication MapParseSchoolOlympiadResultApi(this WebApplication app)
    {
        app.MapPostParseSchoolOlympiadResultApi();
        return app;
    }

    private static WebApplication MapPostParseSchoolOlympiadResultApi(this WebApplication app)
    {
        app.MapPost("/parser", ParseExcel)
            .WithOpenApi(operation =>
            {
                operation.Tags = new[] { new OpenApiTag { Name = "ParserSchoolOlympiad" } };
                operation.Summary = "Парсит результаты олимпиады из Excel-файла и сохраняет их.";
                operation.Responses.Add("500",
                    new OpenApiResponse() { Description = "Данные спарсить и добавить не удалось." });
                operation.Responses.Add("400",
                    new OpenApiResponse() { Description = "Файл не является excel-файлом." });
                operation.Responses["200"].Description = "Данные успешно спарсены и добавлены.";

                return operation;
            })
            .DisableAntiforgery();
        return app;
    }

    /// <summary>Метод для загрузки и парсинга Excel файла.</summary>
    /// <param name="file">Запрос с файлом.</param>
    /// <param name="parser"><see cref="IParser" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>Результат выполнения запроса.</returns>
    private static async Task<IResult> ParseExcel(IFormFile file, IParser parser, CancellationToken cancellationToken)
    {
        if (file.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" &&
            file.ContentType != "application/vnd.ms-excel")
        {
            return TypedResults.BadRequest("Неверный формат файла. Загрузите Excel файл.");
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);
        await parser.ParseAndSaveAsync(stream, cancellationToken);
        // TODO: Добавить middleware по обработке ошибок.

        return TypedResults.Ok();
    }
}