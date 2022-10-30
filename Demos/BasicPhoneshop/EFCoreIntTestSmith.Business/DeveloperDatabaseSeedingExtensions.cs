using System.Diagnostics.CodeAnalysis;

namespace EFCoreIntTestSmith.Business;

[ExcludeFromCodeCoverage]
public static class DeveloperDatabaseSeedingExtensions
{
    public static void SeedForDeveloper(this DataContext context)
    {
        Brand huawei = new() { Name = "Huawei" };
        Brand samsung = new() { Name = "Samsung" };
        Brand apple = new() { Name = "Apple" };
        Brand google = new() { Name = "Google" };
        Brand xiaomi = new() { Name = "Xiaomi" };

        Phone p30 = new()
        {
            Brand = huawei,
            Type = "P30",
            Price = 100M,
            Stock = 20
        };
        Phone galaxy = new()
        {
            Brand = samsung,
            Type = "Galaxy A52",
            Price = 200M,
            Stock = 25
        };
        Phone iPhone = new()
        {
            Brand = apple,
            Type = "iPhone 11",
            Price = 500M,
            Stock = 0
        };
        Phone pixel = new()
        {
            Brand = google,
            Type = "Pixel 4a",
            Price = 400M,
            Stock = 15
        };
        Phone redmi = new()
        {
            Brand = xiaomi,
            Type = "Redmi Note 10 Pro",
            Price = 300M,
            Stock = 3
        };

        List<Phone> phones = new() { p30, galaxy, iPhone, pixel, redmi };
        context.AddRange(phones);
        context.SaveChanges();
    }
}