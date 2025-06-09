 

namespace Catalog.Products.Dtos;

public record ProductDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string[]? Categories { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl { get; set; }
};
