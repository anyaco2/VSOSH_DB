namespace VSOSH.Domain.Services;

/// <summary>
/// Представляет информацию об excel данных.
/// </summary>
public static class ExcelResultInfo
{
	#region Data
	#region Consts
	/// <summary>
	/// Возвращает номер столбца с направлением практики.
	/// </summary>
	public const int ColumnWithDirectionPractice = 9; // I

	/// <summary>
	/// Возвращает номер столбца с итоговым баллом по практике.
	/// </summary>
	public const int ColumnWithFinalScoreInPractice = 13; // M

	/// <summary>
	/// Возвращает номер столбца с итоговым баллом по теории.
	/// </summary>
	public const int ColumnWithFinalScoreInTheory = 11; // K
	/// <summary>
	/// Возвращает номер столбца с названием предмета по олимпиаде.
	/// </summary>
	public const int ColumnWithNameOfSchoolSubject = 1; // A

	/// <summary>
	/// Возвращает номер столбца с кодом участника.
	/// </summary>
	public const int ColumnWithParticipantCode = 3; // C

	/// <summary>
	/// Возвращает номер столбца с предварительным баллом по практике.
	/// </summary>
	public const int ColumnWithPreliminaryScoreInPractice = 12; // L

	/// <summary>
	/// Возвращает номер столбца с предварительным баллом по теории.
	/// </summary>
	public const int ColumnWithPreliminaryScoreInTheory = 10; // J

	/// <summary>
	/// Возвращает номер столбца с названием образовательного учреждения.
	/// </summary>
	public const int ColumnWithSchoolName = 2; // B

	/// <summary>
	/// Возвращает номер столбца с полом ученика.
	/// </summary>
	public const int ColumnWithSexStudent = 7; // G

	/// <summary>
	/// Возвращает номер столбца с именем ученика.
	/// </summary>
	public const int ColumnWithStudentFirstName = 5; // E

	/// <summary>
	/// Возвращает номер столбца с фамилией ученика.
	/// </summary>
	public const int ColumnWithStudentLastName = 4; // D

	/// <summary>
	/// Возвращает номер столбца с отчеством ученика.
	/// </summary>
	public const int ColumnWithStudentMiddleName = 6; // F
	#endregion

	#region Static
	/// <summary>
	/// Возвращает номера столбцов с названием класса, в котором учится ученик.
	/// </summary>
	public static readonly (int BaseCurrentCompeting, int PECurrentCompeting) ColumnWithCurrentCompeting = (8, 9); // H, I

	/// <summary>
	/// Возвращает номера столбцов с итоговым баллом.
	/// </summary>
	public static readonly (int BaseFinalScore, int TechnologyFinalScore, int PEFinalScore) ColumnWithFinalScore = (9, 10, 14); // I, J, N

	/// <summary>
	/// Возвращает номера столбцов с классом, за который выступает.
	/// </summary>
	public static readonly (int BaseGradeCompeting, int PEGradeCompeting) ColumnWithGradeCompeting = (7, 8); // G, H

	/// <summary>
	/// Возвращает номера столбцов с процентом выполнения.
	/// </summary>
	public static readonly (int BasePercentage, int TechnologyPercentage, int PEPercentage) ColumnWithPercentage = (10, 11, 15); // J, K, O

	/// <summary>
	/// Возвращает номера столбцов со статусом ученика.
	/// </summary>
	public static readonly (int BaseStatus, int TechnologyStatus, int PEStatus) ColumnWithStatus = (11, 12, 16); // K, L, P

	/// <summary>
	/// Возвращает возможные начальные строки.
	/// </summary>
	public static readonly (int StartRow, int PeStartRow) StartRow = (15, 16);
	#endregion
	#endregion
}
