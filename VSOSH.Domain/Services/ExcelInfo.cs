namespace VSOSH.Domain.Services;

public class ExcelInfo
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
    /// Возвращает название столбца с названием класса, в котором учится ученик.
    /// </summary>
    public const string ColumnWithCurrentCompeting = "H";

    /// <summary>
    /// Возвращает название столбца с итоговым баллом.
    /// </summary>
    public const string ColumnWithFinalScore = "I";

    /// <summary>
    /// Возвращает название столбца с процентом выполнения.
    /// </summary>
    public const string ColumnWithPercentage = "J";

    /// <summary>
    /// Возвращает название столбца со статусом ученика.
    /// </summary>
    public const string ColumnWithStatus = "K";

    /// <summary>
    /// Возвращает название столбца с полом ученика.
    /// </summary>
    public const string ColumnWithSexStudent = "G";

    /// <summary>
    /// Возвращает возможные начальные строчки.
    /// </summary>
    public static readonly (int startRow, int secondStartRow) StartRow = (15, 16);

    public static readonly (string BaseGradeCompeting, string PEGradeCompeting) ColumnWithGradeCompeting = ("G", "H");
}