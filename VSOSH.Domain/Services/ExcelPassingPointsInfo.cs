namespace VSOSH.Domain.Services;

/// <summary>
/// Представляет информацию об Excel-файле проходных баллов для EPPlus.
/// </summary>
public static class ExcelPassingPointsInfo
{
	#region Data
	#region Consts
	/// <summary>
	/// Возвращает номер столбца с именем ученика.
	/// </summary>
	public const int ColumnWithFirstName = 3; // C

	/// <summary>
	/// Возвращает номер столбца с фамилией ученика.
	/// </summary>
	public const int ColumnWithLastName = 2; // B
	
	/// <summary>
	/// Возвращает номер столбца с отчеством ученика.
	/// </summary>
	public const int ColumnWithMiddleName = 4; // D

	/// <summary>
	/// Возвращает номер столбца с направлением практики.
	/// </summary>
	public const int ColumnWithPractice = 6; // E
	/// <summary>
	/// Возвращает номер столбца с наименованием школы.
	/// </summary>
	public const int ColumnWithSchoolName = 1; // A

	/// <summary>
	/// Возвращает номер столбца с полом ученика.
	/// </summary>
	public const int ColumnWithSex = 5; // D
	#endregion

	#region Static
	/// <summary>
	/// Возвращает номера столбцов с итоговыми баллами.
	/// </summary>
	public static readonly (int Base, int TechnologyOrPe) ColumnWithFinalScore = (7, 8); // F/G

	/// <summary>
	/// Возвращает номера столбцов с классом, за который выступает ученик.
	/// </summary>
	public static readonly (int Base, int Pe) ColumnWithGradeCompeting = (5, 6); // D для базового, E для физры

	/// <summary>
	/// Возвращает номера столбцов с процентным выполнением.
	/// </summary>
	public static readonly (int Base, int TechnologyOrPe) ColumnWithPercentage = (6, 7); // E/F

	/// <summary>
	/// Возвращает номера столбцов со статусом.
	/// </summary>
	public static readonly (int Base, int TechnologyOrPe) ColumnWithStatus = (8, 9); // G/H
	#endregion
	#endregion
}
