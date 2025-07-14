using System.Net;

namespace VSOSH.Contracts.Exceptions;

/// <summary>
/// Представляет исключение, связанное с тем, что во время работы запроса не нашли необходимое.
/// </summary>
/// <param name="message"></param>
public class NotFoundException(string? message) : HttpRequestException(message, null, HttpStatusCode.NotFound);
