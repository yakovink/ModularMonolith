using System;

namespace Basket.Baskets.Dtos;

public record ItemInCartDto
{
    public string? UserName { get; set; }
    public ShoppingCartItemDto? ItemDto { get; set; }

}
