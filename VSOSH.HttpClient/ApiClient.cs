using VSOSH.Contracts;

namespace VSOSH.HttpClient;

public class ApiClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public ApiClient(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5002");
    }
    
    public async Task<string> UploadParserResultAsync(Stream fileStream, string fileName)
    {
        try
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = fileName
            };
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.ms-excel");
            content.Add(fileContent);

            var response = await _httpClient.PostAsync("/parser", content);

            if (response.IsSuccessStatusCode)
            {
                return "Файл успешно загружен.";
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return $"Ошибка загрузки файла: {response.StatusCode}. Детали: {errorContent}";
        }
        catch (HttpRequestException ex)
        {
            return $"Ошибка при выполнении запроса: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Непредвиденная ошибка: {ex.Message}";
        }
    }

    /// <summary>
    /// Возвращает проходные баллы по указанному предмету.
    /// </summary>
    /// <param name="subject">Предмет.</param>
    /// <returns>Проходные баллы по указанному предмету.</returns>
    public async Task<byte[]> GetPassingPointsAsync(Subject subject)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/passingPoints/{subject}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Ошибка получения проходных баллов: {response.StatusCode}. Детали: {errorContent}");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Ошибка при выполнении запроса: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Возвращает общий отчет.
    /// </summary>
    /// <returns>Общий отчет.</returns>
    public async Task<byte[]> GetGeneralReportAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/generalReport");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Ошибка получения общего отчета: {response.StatusCode}. Детали: {errorContent}");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Ошибка при выполнении запроса: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Возвращает количественные данные.
    /// </summary>
    /// <returns>Количественные данные.</returns>
    public async Task<byte[]> GetQuantitativeDataAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/quantitativeData");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Ошибка получения количественных данных: {response.StatusCode}. Детали: {errorContent}");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Ошибка при выполнении запроса: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Возвращает данные за более старший класс.
    /// </summary>
    /// <returns>Данные за более старший класс</returns>
    public async Task<byte[]> GetGreaterClassDataAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/greaterClass");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Ошибка получения данных о старших классах: {response.StatusCode}. Детали: {errorContent}");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Ошибка при выполнении запроса: {ex.Message}", ex);
        }
    }
}
