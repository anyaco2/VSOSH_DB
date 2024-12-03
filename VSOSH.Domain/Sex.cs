namespace VSOSH.Domain;

/// <summary>
/// Пол ученика.
/// </summary>
public enum Sex
{
    /// <summary>
    /// Мужской.
    /// </summary>
    Male,

    /// <summary>
    /// Женский
    /// </summary>
    Female
}

public static class SexExtension
{
    public static string GetString(this Sex sex) =>
        sex switch
        {
            Sex.Male => "М",
            Sex.Female => "Ж",
            _ => throw new ArgumentOutOfRangeException(nameof(sex), sex, null)
        };
}