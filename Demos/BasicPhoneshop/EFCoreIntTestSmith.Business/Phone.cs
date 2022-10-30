using System.Diagnostics.CodeAnalysis;

namespace EFCoreIntTestSmith.Business;

[ExcludeFromCodeCoverage]
public class Phone
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public Brand Brand { get; set; } = null!;

    public int BrandId { get; set; }

    public override string ToString() => $"{Brand.Name} {Type} ({Price})";

    public decimal Price { get; set; }

    public int Stock { get; set; }
}