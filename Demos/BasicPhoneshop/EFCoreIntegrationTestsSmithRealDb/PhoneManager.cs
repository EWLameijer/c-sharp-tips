using EFCoreIntTestSmith.Business;

namespace EFCoreIntTestsSmith.ConsoleApp;

internal class PhoneManager
{
    internal static void Display(IEnumerable<Phone> phones, string? caption = null)
    {
        if (caption != null) Console.WriteLine(caption);
        phones.ToList().ForEach(Display);
    }

    internal static void Display(Phone phone)
    {
        Console.WriteLine($"{phone} : {phone.Stock} in stock.");
    }
}