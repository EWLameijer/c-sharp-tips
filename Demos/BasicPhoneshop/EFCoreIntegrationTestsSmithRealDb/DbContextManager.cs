using EFCoreIntTestSmith.Business;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIntTestsSmith.ConsoleApp;

internal static class DbContextManager
{
    internal static DataContext Get()
    {
        const string ConnectionString = "Data Source=(localdb)\\ProjectModels;" +
        "Initial Catalog=EFCoreIntegrationTest;Integrated Security=True;Connect Timeout=30;" +
        "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
        "MultiSubnetFailover=False";

        DbContextOptionsBuilder<DataContext> builder = new();
        builder.UseSqlServer(ConnectionString);
        DbContextOptions<DataContext> options = builder.Options;
        return new(options);
    }

    // below code to ensure development database is updated after a migration
    internal static void UpdateDevelopmentDatabase()
    {
        DataContext context = Get();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();
        context.SeedForDeveloper();
    }
}