



namespace Account.Users.Models;

public class User : Aggregate<Guid>
{
    public string UserId { get; private set; } = default!;
    public string UserName { get; private set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string address { get; set; } = default!;
    public Guid ShoppingCartId { get; private set; }
    [JsonInclude]
    public List<UserActivity> Activities { get; private set; } = new();


    public static User Create(Guid cartId,string hashedPassword, string userId, string userName,string FirstName, string LastName, string email, string phoneNumber, string address)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentException.ThrowIfNullOrEmpty(userName);
        ArgumentNullException.ThrowIfNull(email);

        ArgumentNullException.ThrowIfNull(phoneNumber);
        ArgumentNullException.ThrowIfNull(address);


        var user = new User
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            UserName = userName,
            FirstName = FirstName,
            LastName = LastName,
            Email = email,
            PhoneNumber = phoneNumber,
            address = address,
            PasswordHash = hashedPassword,
            ShoppingCartId = cartId


        };
        return user;
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


    
    

    
    


    



}
