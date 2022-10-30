using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace EFCoreIntTestSmith.Business.IntegrationTests.PhoneServiceTests;

public class Add
{
    [Fact]
    public void Phone_should_not_be_added_if_brand_type_combo_already_in_database()
    {
        // arrange
        DbContextOptions<DataContext> options = this.CreateUniqueClassOptions<DataContext>();
        using DataContext context = new(options);
        context.DefaultSetup();
        PhoneService phoneService = new(context);
        Phone iPhone = new() { Brand = new Brand { Name = "Apple" }, Type = "iPhone 11" };

        // act
        Action shouldThrow = () => phoneService.Add(iPhone);

        // assert
        Assert.Throws<ArgumentException>(shouldThrow);
    }

    [Fact]
    public void Existing_brand_should_not_be_added()
    {
        // arrange
        DbContextOptions<DataContext> options = this.CreateUniqueClassOptions<DataContext>();
        using DataContext context = new(options);
        context.DefaultSetup();
        PhoneService phoneService = new(context);
        string brandName = "Apple";
        string typeName = "iPhone 13";
        Phone iPhone13 = new() { Brand = new Brand { Name = brandName }, Type = typeName };

        // act
        phoneService.Add(iPhone13);

        // assert
        int newBrandCount = phoneService.GetBrands().Count();
        Assert.Equal(2, newBrandCount);
        Assert.Equal(4, phoneService.GetUsingIEnumerable().Count());
        Assert.NotNull(phoneService.GetByBrandAndType(brandName, typeName));
    }

    [Fact]
    public void Adding_phone_with_new_brand_creates_both_phone_and_brand()
    {
        // arrange
        DbContextOptions<DataContext> options = this.CreateUniqueClassOptions<DataContext>();
        using DataContext context = new(options);
        context.DefaultSetup();
        PhoneService phoneService = new(context);

        // act
        string brandName = "Pear";
        string typeName = "MePhone 1";
        Phone mePhone1 = new() { Brand = new Brand { Name = brandName }, Type = typeName };
        phoneService.Add(mePhone1);

        // assert
        int newBrandCount = phoneService.GetBrands().Count();
        Assert.Equal(3, newBrandCount);
        Assert.Equal(4, phoneService.GetUsingIEnumerable().Count());
        Assert.NotNull(phoneService.GetByBrandAndType(brandName, typeName));
    }
}