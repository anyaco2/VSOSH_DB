namespace VSOSH.Domain.Services;

/// <summary>
/// Представляет информацию об Excel-файле проходных баллов.
/// </summary>
public static class ExcelPassingPointsInfo
{
    /// <summary>
    /// Возвращает название столбца с наименованием школы.
    /// </summary>
    public const string ColumnWithSchoolName = "A";

    /// <summary>
    /// Возвращает название столбца с фамилией ученика.
    /// </summary>
    public const string ColumnWithLastName = "B";

    /// <summary>
    /// Возвращает название столбца именем ученика.
    /// </summary>
    public const string ColumnWithFirstName = "C";

    /// <summary>
    /// Возвращает название столбца с полом ученика.
    /// </summary>
    public const string ColumnWithSex = "D";

    /// <summary>
    /// Возвращает название столбца с направлением практики.
    /// </summary>
    public const string ColumnWithPractice = "E";

    /// <summary>
    /// Возвращает название столбца клалса, за который выступает ученик.
    /// </summary>
    public static readonly (string Base, string Pe) ColumnWithGradeCompeting = ("D", "E");

    /// <summary>
    /// Возвращает название столбца процентным выполнением.
    /// </summary>
    public static readonly (string Base, string TechnologyOrPe) ColumnWithPercentage = ("E", "F");

    /// <summary>
    /// Возвращает название столбца с итоговыми баллами.
    /// </summary>
    public static readonly (string Base, string TechnologyOrPe) ColumnWithFinalScore = ("F", "G");

    /// <summary>
    /// Возвращает название столбца со статусом.
    /// </summary>
    public static readonly (string Base, string TechnologyOrPe) ColumnWithStatus = ("G", "H");
}