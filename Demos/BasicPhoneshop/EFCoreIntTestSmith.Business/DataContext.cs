using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIntTestSmith.Business;

[ExcludeFromCodeCoverage]
public class DataContext : DbContext
{
    public DbSet<Brand> Brands { get; set; } = null!;

    public DbSet<Phone> Phones { get; set; } = null!;

    public DataContext()
    { }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            const string ConnectionString = "Data Source=(localdb)\\ProjectModels;" +
            "Initial Catalog=EFCoreIntegrationTest;Integrated Security=True;Connect Timeout=30;" +
            "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Phone>().Property(x => x.Price).HasPrecision(9, 2);
    }
}