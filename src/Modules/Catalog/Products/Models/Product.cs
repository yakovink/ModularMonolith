using Microsoft.AspNetCore.Identity;

namespace Catalog.Products.Models;

public class Product:Aggregate<Guid>   
{
    public string Name { get; private set; }=default!;
    public List<ProductCategory> Categories { get; private set; }=new();
    public decimal Price { get; private set; }
    public string Description { get; private set; }=default!;
    public string ImageFile { get;private set; }=default!;

    public static Product Create(Guid id, string name, List<ProductCategory> categories, decimal price, string description, string imageFile)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var product = new Product
        {
            Id = id,
            Name = name,
            Categories = categories,
            Price = price,
            Description = description,
            ImageFile = imageFile,
            _createdBy=Environment.UserName,
            _createdDate=DateTime.Now,
            _lastModifiedBy=Environment.UserName,
            _lastModifiedDate=DateTime.Now
        };
        product.AddDomainEvent(new ProductCreatedEvent(product));
        return product;
    }

    public void Update(string name, List<ProductCategory> categories, decimal price, string description, string imageFile)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        Name = name;
        Categories = categories;
        Description = description;
        ImageFile = imageFile;

        if (Price != price)
        {
            Price = price;
            AddDomainEvent(new ProductPriceChangedEvent(this));
        }
        _lastModifiedBy=Environment.UserName;
        _lastModifiedDate=DateTime.Now;
    }

}
