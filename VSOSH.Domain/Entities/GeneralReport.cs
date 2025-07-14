namespace VSOSH.Domain.Entities;

/// <summary>
/// Представляет общий отчет.
/// </summary>
/// <param name="TotalCount">Кол-во фактов участия.</param>
/// <param name="UniqueParticipants">Кол-во уникальных участников.</param>
/// <param name="TotalWinnerDiplomas">Кол-во победителей.</param>
/// <param name="UniqueWinners">Кол-во уникальных победителей.</param>
/// <param name="TotalPrizeDiplomas">Кол-во призеров.</param>
/// <param name="UniquePrizeWinners">Кол-во уникальных призеров.</param>
/// <param name="UniqueWinnersAndPrizeWinners">Сумма уникальных призеров и победителей.</param>
public record GeneralReport(
	int TotalCount,
	int UniqueParticipants,
	int TotalWinnerDiplomas,
	int UniqueWinners,
	int TotalPrizeDiplomas,
	int UniquePrizeWinners,
	int UniqueWinnersAndPrizeWinners);
