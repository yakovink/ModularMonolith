using System;

namespace Account.Users.Dtos;

public record UserDto
{
    public Guid? Id { get; set; }
    public Guid? ShoppingCartId { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    

}
