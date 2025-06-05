using System;
using System.Threading.Tasks;




namespace Account.Users.Models;

public class User : Aggregate<Guid>
{
    public string UserId { get; private set; } = default!;
    public string UserName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public string address { get; private set; } = default!;
    public Guid ShoppingCartId { get; private set; }
    public bool IsActive { get; private set; } = false;
    public DateTime? LastLogin { get; private set; }
    public DateTime? LastLogout { get; private set; }
    public IPAddress? LastLoginIp { get; private set; }



    public static async Task<User> Create(Guid id, string userId, string userName,string FirstName, string LastName, string email, string phoneNumber, Address address, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentException.ThrowIfNullOrEmpty(userName);
        ArgumentNullException.ThrowIfNull(email);

        ArgumentNullException.ThrowIfNull(phoneNumber);
        ArgumentNullException.ThrowIfNull(address);

        var user = new User
        {
            Id = id,
            UserId = userId,
            UserName = userName,
            FirstName = FirstName,
            LastName = LastName,
            Email = email,
            PasswordHash = null!,
            PhoneNumber = phoneNumber,
            address = address.ToString(),
            _createdBy=Environment.UserName,
            _createdDate=DateTime.UtcNow,
            _lastModifiedBy=Environment.UserName,
            _lastModifiedDate=DateTime.UtcNow
        };
        user.PasswordHash = user.hashPassword(user.UserId); // Hashing UserId as a placeholder for password
        user.ShoppingCartId = await user.CreateShoppingCartAsync(cancellationToken);
        return user;
    }

    public void Update(string firstName, string lastName, string phoneNumber, Address address, string email)
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

    public void UpdatePassword(string? newPassword)
    {
        ArgumentException.ThrowIfNullOrEmpty(newPassword);
        if (!validateCredentials(newPassword))
            PasswordHash = hashPassword(newPassword);
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
        JsonDocument response = await controller.Post("baskets/create", new { input = new { UserName = this.UserName } }, cancellationToken);
        Console.WriteLine(response.RootElement.ToString());
        JsonElement id = response.RootElement.GetProperty("output");
        return Guid.Parse(id.GetString()!);

    }

    public bool validateCredentials(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty", nameof(password));
        }

        // Here you would typically use a hashing algorithm to compare the stored hash with the hash of the provided password
        return PasswordHash == hashPassword(password); // Simplified for demonstration purposes
    }

    public string hashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty", nameof(password));
        }
        string hashstring = $"{password}{_createdDate}"; // Placeholder for actual hashing logic
        // Here you would typically use a hashing algorithm to hash the password
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashstring));
            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
        // For demonstration purposes, we will just return the password itself
    }

    public UserDto ToDto()
    {
        return new UserDto
        {
            Id = Id,
            ShoppingCartId=ShoppingCartId,
            UserId = UserId,
            UserName = UserName,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Address = address
        };
    }

    public async static Task<User> fromDto(UserDto userDto, CancellationToken cancellationToken)
    {
        if (userDto == null)
        {
            throw new ArgumentNullException(nameof(userDto), "UserDto cannot be null");
        }
        string[] addressParts = userDto.Address?.Split(',', StringSplitOptions.TrimEntries) ?? throw new ArgumentException("Address cannot be null or empty");
        User user = await Create(
            userDto.Id ?? new Guid(),
            userDto.UserId ?? throw new ArgumentException("UserId cannot be null"),
            userDto.UserName ?? throw new ArgumentException("UserName cannot be null"),
            userDto.FirstName ?? throw new ArgumentException("FirstName cannot be null"),
            userDto.LastName ?? throw new ArgumentException("LastName cannot be null"),
            userDto.Email ?? throw new ArgumentException("Email cannot be null"),
            userDto.PhoneNumber ?? throw new ArgumentException("PhoneNumber cannot be null"),
            Address.Create(
                Guid.NewGuid(),
                addressParts[0], // Street
                addressParts.Length > 1 ? addressParts[1] : throw new ArgumentException("City cannot be null"),
                addressParts.Length > 2 ? addressParts[2] : throw new ArgumentException("State cannot be null"),
                addressParts.Length > 3 ? addressParts[3] : throw new ArgumentException("ZipCode cannot be null"),
                addressParts.Length > 4 ? addressParts[4] : throw new ArgumentException("Country cannot be null")
            ),
            cancellationToken
        );
        Console.WriteLine($"User created from DTO: {user.UserName}, Email: {user.Email}");
        return user;
    }
    
    

    
    


    



}
