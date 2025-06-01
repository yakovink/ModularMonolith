using System;
using System.Net;
using System.Text.Json;



namespace Account.Users.Models;

public class User : Aggregate<Guid>
{
    public string UserId { get; private set; } = default!;
    public string UserName { get; private set; } = default!;
    public string? Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public string address { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public Guid ShoppingCartId { get; private set; } = default!;
    public bool IsActive { get; private set; } = false;
    public DateTime LastLogin { get; private set; } = default!;
    public DateTime LastLogout { get; private set; } = default;
    public IPAddress LastLoginIp { get; private set; } = default!;



    public static User Create(Guid id, string userId, string userName, string email, string passwordHash, string phoneNumber, Address address)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentException.ThrowIfNullOrEmpty(userName);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentException.ThrowIfNullOrEmpty(passwordHash);
        ArgumentNullException.ThrowIfNull(phoneNumber);
        ArgumentNullException.ThrowIfNull(address);

        var user = new User
        {
            Id = id,
            UserId = userId,
            UserName = userName,
            Email = email,
            PasswordHash = passwordHash,
            PhoneNumber = phoneNumber,
            address = address.ToString()
        };

        return user;
    }

    public void Update(string firstName, string lastName, string phoneNumber, Address address, string email )
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName);
        ArgumentException.ThrowIfNullOrEmpty(lastName);
        ArgumentNullException.ThrowIfNull(phoneNumber);
        ArgumentNullException.ThrowIfNull(address);
        ArgumentNullException.ThrowIfNull(email);

        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        this.address = address.ToString();
        Email = email;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        ArgumentException.ThrowIfNullOrEmpty(newPasswordHash);
        PasswordHash = newPasswordHash;
    }


    public void login(IPAddress ipAddress)
    {
        if (ipAddress == null)
        {
            throw new ArgumentNullException(nameof(ipAddress), "IP address cannot be null");
        }

        LastLogin = DateTime.UtcNow;
        LastLoginIp = ipAddress;
        IsActive = true;
    }

    public void logout()
    {
        LastLogout = DateTime.Now;
        IsActive = false;
    }
    public async Task<Guid> CreateShoppingCartAsync(CancellationToken cancellationToken)
    {
        HttpController controller = Constants.AccountController;
        JsonDocument response = await controller.Post("basket/create", new { UserId = this.UserId });
        JsonElement data = response.RootElement.GetProperty("data");
        if (data.ValueKind != JsonValueKind.Object || !data.TryGetProperty("id", out JsonElement idProperty))
        {
            throw new Exception("Invalid response format");
        }
        return Guid.Parse(idProperty.GetString()!);

    }
    
    

    
    


    



}
