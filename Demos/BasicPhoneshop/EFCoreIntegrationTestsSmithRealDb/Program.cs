// integration tests
// EF Core
// real database
// method: Smith

// See https://aka.ms/new-console-template for more information
using EFCoreIntTestSmith.Business;
using EFCoreIntTestsSmith.ConsoleApp;

Console.WriteLine("Start program!");
DataContext context = DbContextManager.Get();
PhoneService phoneService = new(context);

IEnumerable<Phone> ienumerablePhones = phoneService.GetUsingIEnumerable();
IQueryable<Phone> iqueryablePhones = phoneService.GetUsingIQueryable();

PhoneManager.Display(ienumerablePhones, "Phones (via IEnumerable)");
PhoneManager.Display(iqueryablePhones, "Phones (via IQueryable)");
Console.WriteLine("End program!");