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
        modelBuilder.Entity<RussianResult>()
            .ToTable("RussianResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<SocialStudiesResult>()
            .ToTable("SocialStudiesResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<MathResult>()
            .ToTable("MathResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<PhysicResult>()
            .ToTable("PhysicResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<BiologyResult>()
            .ToTable("BiologyResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<ChemistryResult>()
            .ToTable("ChemistryResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<ComputerScienceResult>()
            .ToTable("ComputerScienceResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<EcologyResult>()
            .ToTable("EcologyResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<EconomyResult>()
            .ToTable("EconomyResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<EnglishResult>()
            .ToTable("EnglishResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<FrenchResult>()
            .ToTable("FrenchResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<GermanResult>()
            .ToTable("GermanResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<ChineseResult>()
            .ToTable("ChineseResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<ArtResult>()
            .ToTable("ArtResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<AstronomyResult>()
            .ToTable("AstronomyResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<GeographyResult>()
            .ToTable("GeographyResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<HistoryResult>()
            .ToTable("HistoryResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<LawResult>()
            .ToTable("LawResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<LiteratureResult>()
            .ToTable("LiteratureResults")
            .HasBaseType<SchoolOlympiadResultBase>();

        modelBuilder.Entity<FundamentalsLifeSafetyResult>()
            .ToTable("FundamentalsLifeSafetyResults")
            .HasBaseType<SchoolOlympiadResultBase>();
    }

    #endregion
}