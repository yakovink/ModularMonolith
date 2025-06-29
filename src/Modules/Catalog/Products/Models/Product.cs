
namespace Catalog.Products.Models;

public class Product:Aggregate<Guid>   
{
    public string Name { get; set; }=default!;
    public List<ProductCategory> Categories { get; set; }=new();
    public decimal Price { get; set; }
    public string Description { get; set; }=default!;
    public string ImageFile { get;set; }=default!;

    public static Product Create(string name, List<ProductCategory> categories, decimal price, string description, string imageFile)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Categories = categories,
            Price = price,
            Description = description,
            ImageFile = imageFile
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
        _lastModifiedDate=DateTime.UtcNow;
    }


    public bool Compare(Product product)
    {
        return Id == product.Id &&
               Name == product.Name &&
               Categories.SequenceEqual(product.Categories) &&
               Price == product.Price &&
               Description == product.Description &&
               ImageFile == product.ImageFile;
    }

    public override string ToString()
    {
        return $"Product: {Id}, {Name}, {Price}, {Description}, {ImageFile}";
    }
    public override bool Equals(object? obj)
    {
        if (obj is Product product)
        {
            return Id == product.Id &&
                   Name == product.Name &&
                   Categories.SequenceEqual(product.Categories) &&
                   Price == product.Price &&
                   Description == product.Description &&
                   ImageFile == product.ImageFile;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Categories, Price, Description, ImageFile);
    }

}
