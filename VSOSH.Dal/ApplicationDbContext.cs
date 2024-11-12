using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VSOSH.Domain.Entities;

namespace VSOSH.Dal;

/// <summary>
/// Представляет <see cref="DbContext" /> для доступа к данным.
/// </summary>
public class ApplicationDbContext : DbContext
{
    #region Data

    /// <summary>Строчка подключения к базе данных.</summary>
    private readonly string? _connectionString;

    #endregion

    #region Statics

    /// <summary>Возвращает строчку подключения к базе данных по-умолчанию.</summary>
    /// <value>Строчка подключения к базе данных по-умолчанию.</value>
    public static string? DefaultConnectionString { get; }

    #endregion

    #region .ctor

    /// <summary>Инициализирует тип <see cref="ApplicationDbContext"/>.</summary>
    static ApplicationDbContext()
    {
        DefaultConnectionString = null;
    }

    /// <summary>Создаёт экземпляр <see cref="ApplicationDbContext"/>.</summary>
    public ApplicationDbContext()
    {
        _connectionString = DefaultConnectionString;
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ApplicationDbContext" />.
    /// </summary>
    /// <param name="options">Настройки текущего контекста.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Вовзращает данные о результатах олимпиады.
    /// </summary>
    public DbSet<SchoolOlympiadResultBase> SchoolOlympiadResultBases { get; } = null!;

    #region Helpers

    private DbSet<ArtResult> ArtResults { get; } = null!;

    private DbSet<AstronomyResult> AstronomyResults { get; } = null!;

    private DbSet<BiologyResult> BiologyResults { get; } = null!;

    private DbSet<ChemistryResult> ChemistryResults { get; } = null!;

    private DbSet<ChineseResult> ChineseResults { get; } = null!;

    private DbSet<ComputerScienceResult> ComputerScienceResults { get; } = null!;

    private DbSet<EcologyResult> EcologyResults { get; } = null!;

    private DbSet<EconomyResult> EconomyResults { get; } = null!;

    private DbSet<EnglishResult> EnglishResults { get; } = null!;

    private DbSet<FrenchResult> FrenchResults { get; } = null!;

    private DbSet<FundamentalsLifeSafetyResult> FundamentalsLifeSafetyResults { get; } = null!;

    private DbSet<GermanResult> GermanResults { get; } = null!;

    private DbSet<GeographyResult> GeographyResults { get; } = null!;

    private DbSet<HistoryResult> HistoryResults { get; } = null!;

    private DbSet<LawResult> LawResults { get; } = null!;

    private DbSet<LiteratureResult> LiteratureResults { get; } = null!;

    private DbSet<MathResult> MathResults { get; } = null!;

    private DbSet<PhysicResult> PhysicResults { get; } = null!;

    private DbSet<RussianResult> RussianResults { get; } = null!;

    private DbSet<SocialStudiesResult> SocialStudiesResults { get; } = null!;

    #endregion

    #endregion

    #region Overrided

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ArgumentNullException.ThrowIfNull(nameof(optionsBuilder));

        base.OnConfiguring(optionsBuilder);

        if (optionsBuilder.IsConfigured) return;

        optionsBuilder.UseNpgsql(_connectionString ?? DefaultConnectionString);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            type => type.Namespace!.StartsWith(GetType()
                .Namespace!));
    }

    #endregion
}