using Microsoft.EntityFrameworkCore;

namespace EFCoreIntTestSmith.Business;

public class PhoneService
{
    private readonly DataContext _context;

    public PhoneService(DataContext context)
    {
        _context = context;
    }

    public Phone? Get(int id) =>
        _context.Phones.Include(p => p.Brand).SingleOrDefault(p => p.Id == id);

    public IEnumerable<Phone> GetUsingIEnumerable() => _context.Phones;

    public IQueryable<Phone> GetUsingIQueryable() => _context.Phones;

    public Phone? GetByBrandAndType(string brandName, string typeName) =>
        _context.Phones.Include(p => p.Brand).SingleOrDefault(
            p => p.Type == typeName && p.Brand.Name == brandName);

    public IEnumerable<Brand> GetBrands() => _context.Brands;

    public void Add(Phone phone)
    {
        IEnumerable<Phone> allPhones = GetUsingIEnumerable();
        string brandToCreate = phone.Brand.Name;
        if (allPhones.Any(p => p.Type == phone.Type && p.Brand.Name == brandToCreate))
            throw new ArgumentException("There is already a phone with that name in the database");
        Brand? existingBrand = _context.Brands.FirstOrDefault(b => b.Name == brandToCreate);
        if (existingBrand != null) phone.Brand = existingBrand;
        _context.Phones.Add(phone);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        Phone phoneToDelete = _context.Phones.Single(p => p.Id == id);
        int brandId = phoneToDelete.BrandId;
        int numPhonesWithBrand = GetUsingIEnumerable().Count(p => p.BrandId == brandId);
        _context.Phones.Remove(phoneToDelete);
        if (numPhonesWithBrand == 1)
        {
            Brand brandToRemove = _context.Brands.Find(brandId)!;
            _context.Brands.Remove(brandToRemove);
        }
        _context.SaveChanges();
    }
}