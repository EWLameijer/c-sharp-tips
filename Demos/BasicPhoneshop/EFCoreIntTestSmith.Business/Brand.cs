using System.Diagnostics.CodeAnalysis;

namespace EFCoreIntTestSmith.Business;

[ExcludeFromCodeCoverage]
public class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}