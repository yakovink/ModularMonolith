using System;

namespace Account.Users.Dtos;

public record PasswordDto
{
    public Guid? Id { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}
