namespace VSOSH.Domain.Services;

/// <summary>
/// Представляет информацию об excel данных.
/// </summary>
public static class ExcelInfo
{
    /// <summary>
    /// Возвращает название столбца с названием предмета по олимпиаде.
    /// </summary>
    public const string ColumnWithNameOfSchoolSubject = "A";

    /// <summary>
    /// Возвращает название столбца с названием образовательного учреждения.
    /// </summary>
    public const string ColumnWithSchoolName = "B";

    /// <summary>
    /// Возвращает название столбца с кодом участника.
    /// </summary>
    public const string ColumnWithParticipantCode = "C";

    /// <summary>
    /// Возвращает название столбца с фамилией ученика.
    /// </summary>
    public const string ColumnWithStudentLastName = "D";

    /// <summary>
    /// Возвращает название столбца с именем ученика.
    /// </summary>
    public const string ColumnWithStudentFirstName = "E";

    /// <summary>
    /// Возвращает название столбца с отчеством ученика.
    /// </summary>
    public const string ColumnWithStudentMiddleName = "F";

    /// <summary>
    /// Возвращает название столбца с полом ученика.
    /// </summary>
    public const string ColumnWithSexStudent = "G";

    /// <summary>
    /// Возвращает название столбца с предварительный баллом по теории.
    /// </summary>
    public const string ColumnWithPreliminaryScoreInTheory = "J";

    /// <summary>
    /// Возвращает название столбца с итоговым баллом по теории.
    /// </summary>
    public const string ColumnWithFinalScoreInTheory = "K";

    /// <summary>
    /// Возвращает название столбца с предварительный баллом по практике.
    /// </summary>
    public const string ColumnWithPreliminaryScoreInPractice = "L";

    /// <summary>
    /// Возвращает название столбца с итоговым баллом по практике.
    /// </summary>
    public const string ColumnWithFinalScoreInPractice = "M";

    /// <summary>
    /// Возвращает название столбца с напрвлением практики.
    /// </summary>
    public const string ColumnWithDirectionPractice = "I";

    /// <summary>
    /// Возвращает название столбца с названием класса, в котором учится ученик.
    /// </summary>
    public static readonly (string BaseCurrentCompeting, string PECurrentCompeting) ColumnWithCurrentCompeting =
        ("H", "I");

    /// <summary>
    /// Возвращает название столбца с итоговым баллом.
    /// </summary>
    public static readonly (string BaseFinalScore, string TechnologyFinalScore, string PEFinalScore)
        ColumnWithFinalScore = ("I", "J", "N");

    /// <summary>
    /// Возвращает название столбца с процентом выполнения.
    /// </summary>
    public static readonly (string BasePercentage, string TechnologyPercentage, string PEPercentage)
        ColumnWithPercentage = ("J", "K", "O");

    /// <summary>
    /// Возвращает название столбца со статусом ученика.
    /// </summary>
    public static readonly (string BaseStatus, string TechnologyStatus, string PEStatus) ColumnWithStatus =
        ("K", "L", "P");

    /// <summary>
    /// Возвращает возможные начальные строчки.
    /// </summary>
    public static readonly (int StartRow, int PeStartRow) StartRow = (15, 16);

    /// <summary>
    /// Возвращает название с столбца с классом, за который выступает.
    /// </summary>
    public static readonly (string BaseGradeCompeting, string PEGradeCompeting) ColumnWithGradeCompeting = ("G", "H");
}