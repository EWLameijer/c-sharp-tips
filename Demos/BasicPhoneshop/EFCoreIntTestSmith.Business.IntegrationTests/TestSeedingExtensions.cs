using TestSupport.EfHelpers;

namespace EFCoreIntTestSmith.Business.IntegrationTests;

internal static class TestSeedingExtensions
{
    public static void TwoApplesAndAGoogle(this DataContext context)
    {
        Brand apple = new() { Name = "Apple" };
        Brand google = new() { Name = "Google" };

        Phone iPhone11 = new()
        {
            Brand = apple,
            Type = "iPhone 11",
            Price = 500M,
            Stock = 0
        };
        Phone iPhone12 = new()
        {
            Brand = apple,
            Type = "iPhone 12",
            Price = 1000M,
            Stock = 0
        };
        Phone pixel = new()
        {
            Brand = google,
            Type = "Pixel 4a",
            Price = 400M,
            Stock = 15
        };

        List<Phone> phones = new() { iPhone11, iPhone12, pixel };
        context.AddRange(phones);
        context.SaveChanges();
    }

    public static void DefaultSetup(this DataContext context)
    {
        context.Database.EnsureClean();
        context.TwoApplesAndAGoogle();
        context.ChangeTracker.Clear();
    }
}