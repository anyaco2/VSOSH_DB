namespace VSOSH.Domain;

/// <summary>
/// Статус ученика на олимпиаде.
/// </summary>
public enum Status
{
    /// <summary>
    /// Призер.
    /// </summary>
    Awardee,

    /// <summary>
    /// Победитель.
    /// </summary>
    Winner,

    /// <summary>
    /// Участник.
    /// </summary>
    Participant
}

public static class StatusExtension
{
    /// <summary>
    /// Вовзращает статус в строковом представлении.
    /// </summary>
    /// <param name="status"><see cref="Status" />.</param>
    /// <returns>Статус в строковом представлении.</returns>
    public static string GetString(this Status status) =>
        status switch
        {
            Status.Awardee => "Призер",
            Status.Winner => "Победитель",
            Status.Participant => "Участник",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}