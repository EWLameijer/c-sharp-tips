using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace EFCoreIntTestSmith.Business.IntegrationTests.PhoneServiceTests;

public class Get
{
    [Fact]
    public void Get_should_return_existing_phone()
    {
        // arrange
        DbContextOptions<DataContext> options = this.CreateUniqueClassOptions<DataContext>();
        using DataContext context = new(options);
        context.DefaultSetup();
        PhoneService phoneService = new(context);

        // act
        Phone phone = phoneService.Get(1)!;

        // assert
        Assert.Equal("iPhone 11", phone.Type);
        Assert.Equal("Apple", phone.Brand.Name);
    }

    [Fact]
    public void Get_with_nonexisting_id_should_return_null()
    {
        // arrange
        DbContextOptions<DataContext> options = this.CreateUniqueClassOptions<DataContext>();
        using DataContext context = new(options);
        context.DefaultSetup();
        PhoneService phoneService = new(context);

        // act
        Phone? phone = phoneService.Get(6);

        // assert
        Assert.Null(phone);
    }
}