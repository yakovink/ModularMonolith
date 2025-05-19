namespace Catalog.Data.Seed;

public class InitialData
{
    private static readonly string[] ProductNames = new[]
    {
        "Ergonomic Keyboard", "Gaming Mouse", "LED Monitor", "Wireless Headphones",
        "USB-C Hub", "Mechanical Keyboard", "Laptop Stand", "Webcam Pro"
    };

    private static List<ProductCategory> GetRandomCategories()
    {
        var allCategories = Enum.GetValues<ProductCategory>().ToList();
        var random = new Random();
        var categoryCount = random.Next(1, 4); // 1-3 categories per product
        return allCategories
            .OrderBy(x => random.Next())
            .Take(categoryCount)
            .ToList();
    }

    private static string GetRandomProductName()
    {
        return ProductNames[new Random().Next(ProductNames.Length)];
    }

    public static IEnumerable<Product> Products => new List<Product>
    {
        //Product.Create(Guid.NewGuid(), GetRandomProductName(), GetRandomCategories(), 100, "Description 1", "ImageUrl1"),
        //Product.Create(Guid.NewGuid(), GetRandomProductName(), GetRandomCategories(), 200, "Description 2", "ImageUrl2"),
        //Product.Create(Guid.NewGuid(), GetRandomProductName(), GetRandomCategories(), 300, "Description 3", "ImageUrl3"),
        //Product.Create(Guid.NewGuid(), GetRandomProductName(), GetRandomCategories(), 400, "Description 4", "ImageUrl4"),
    };
}
