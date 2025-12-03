namespace VSOSH.Domain;

public enum Subject
{
	Art,
	Astronomy,
	Biology,
	Chemistry,
	Chinese,
	ComputerScience,
	Ecology,
	Economy,
	English,
	Spanish,
	French,
	FundamentalsLifeSafety,
	Geography,
	German,
	History,
	Law,
	Literature,
	Math,
	PhysicalEducation,
	Physic,
	Russian,
	SocialStudies,
	Technology,
	Italian
}

public static class SubjectExtension
{
	#region Public
	/// <summary>
	/// Вовзращает предмет в строковом представлении.
	/// </summary>
	/// <param name="subject"><see cref="Status" />.</param>
	/// <returns>Статус в строковом представлении.</returns>
	public static string GetString(this Subject subject) =>
		subject switch
		{
			Subject.Math => "математика",
			Subject.Russian => "русский_язык",
			Subject.Geography => "география",
			Subject.Literature => "литература",
			Subject.English => "английский_язык",
			Subject.FundamentalsLifeSafety => "основы_безопасности_и_защиты_родины",
			Subject.Technology => "труды",
			Subject.PhysicalEducation => "физическая_культура",
			Subject.Chinese => "китайский_язык",
			Subject.German => "немецкий_язык",
			Subject.Astronomy => "астрономия",
			Subject.Biology => "биология",
			Subject.ComputerScience => "информатика",
			Subject.History => "история",
			Subject.Art => "искусство_мхк",
			Subject.SocialStudies => "обществознание",
			Subject.Law => "право",
			Subject.Physic => "физика",
			Subject.French => "французский_язык",
			Subject.Ecology => "экология",
			Subject.Economy => "экономика",
			Subject.Chemistry => "химия",
			Subject.Spanish => "испанский_язык",
			Subject.Italian => "итальянский_язык",
			_ => throw new ArgumentOutOfRangeException(nameof(subject), subject, null)
		};
	#endregion
}
