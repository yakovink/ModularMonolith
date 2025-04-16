using System;

namespace Catalog.Products.Dtos;

public record ProductDto
(
    Guid Id,
    string Name,
    List<ProductCategory> Categories,
    string Description,
    decimal Price,
    string ImageUrl
);
