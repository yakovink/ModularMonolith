using System;

namespace Basket.Baskets.Dtos;

public record ShoppingCartDto
{
    public Guid? Id { get; set; }
    public string? UserName { get; set; }
    public List<ShoppingCartItemDto>? Items { get; set; }
}
