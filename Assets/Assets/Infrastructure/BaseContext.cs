using Assets.Catalog;
using Assets.Maintenance;
using Microsoft.EntityFrameworkCore;

namespace Assets.Infrastructure;

public class BaseContext: DbContext
{
    public BaseContext(DbContextOptions<BaseContext> options)
        : base(options)
    {
    }
    
    public BaseContext() : base()
    {
    }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")).UseSnakeCaseNamingConvention();*/
    
    public DbSet<Catalog.Asset> Assets { get; set; } = null!;
    public DbSet<Defect> Defects { get; set; } = null!;
    public DbSet<DefectCategory> DefectCategories { get; set; } = null!;
    public DbSet<DefectStatus> DefectStatuses { get; set; } = null!;
    public DbSet<Failure> Failures { get; set; } = null!;
    public DbSet<FailureStatus> FailureStatuses { get; set; } = null!;
    public DbSet<FailureCategory> FailureCategories { get; set; } = null!;
    public DbSet<TechMap> TechMaps { get; set; } = null!;
    public DbSet<Work> Works { get; set; } = null!;
    public DbSet<WorkStatus> WorkStatuses { get; set; } = null!;
}