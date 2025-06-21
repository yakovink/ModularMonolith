




using Shared.Data;

namespace Account.Data.Repositories;

public abstract class AccountLocalRepository<R>(R repository) : AccountModuleStructre.AccountRepository<R>(repository), IAccountRepository 
    where R : class , IGenericRepository<User>
{


    public async Task<bool> ChangePassword(PasswordDto passDto, CancellationToken cancellationToken = default)
    {

        var id = passDto.Id ?? throw new ArgumentNullException(nameof(passDto));
        var oldPasswordHash = passDto.OldPassword ?? throw new ArgumentNullException(nameof(passDto.OldPassword));
        var newPasswordHash = passDto.NewPassword ?? throw new ArgumentNullException(nameof(passDto.NewPassword));
        var confirmPasswordHash = passDto.ConfirmPassword ?? throw new ArgumentNullException(nameof(passDto.ConfirmPassword));
        if (newPasswordHash.Equals(oldPasswordHash))
        {
            throw new ArgumentException("New password cannot be the same as the old password.");
        }
        if (!newPasswordHash.Equals(confirmPasswordHash))
        {
            throw new ArgumentException("New password and confirm password do not match.");
        }
        User user = await GetUserById(id, false, cancellationToken);
        if (!user.PasswordHash.Equals(passDto.OldPassword))
        {
            throw new ArgumentException("Old password does not match the current password.");
        }

        user.PasswordHash = newPasswordHash;
        await repository.SaveChangesAsync(id, cancellationToken);
        return true;
    }

    public async Task<User> CreateUser(UserDto userDto, CancellationToken cancellationToken = default)
    {

        JsonDocument response = await Constants.AccountController.Post("baskets/create", new { input = new { } }, cancellationToken);
        Console.WriteLine(response.RootElement.ToString());
        JsonElement cartid = response.RootElement.GetProperty("output");
        Guid cartId= Guid.Parse(cartid.GetString()!);


        User user = User.Create(
            cartId,
            userDto.PasswordHash ?? throw new ArgumentNullException(nameof(userDto.PasswordHash), "Password Hash cannot be null"),
            userDto.UserId ?? throw new ArgumentNullException(nameof(userDto.UserId), "User ID cannot be null"),
            userDto.UserName ?? throw new ArgumentNullException(nameof(userDto.UserName), "User Name cannot be null"),
            userDto.FirstName ?? throw new ArgumentNullException(nameof(userDto.FirstName), "First Name cannot be null"),
            userDto.LastName ?? throw new ArgumentNullException(nameof(userDto.LastName), "Last Name cannot be null"),
            userDto.Email ?? throw new ArgumentNullException(nameof(userDto.Email), "Email cannot be null"),
            userDto.PhoneNumber ?? throw new ArgumentNullException(nameof(userDto.PhoneNumber), "Phone Number cannot be null"),
            userDto.Address ?? throw new ArgumentNullException(nameof(userDto.Address), "Address cannot be null")
        );

        return  await repository.CreateElement(user, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUserByCondition(Expression<Func<User, bool>> condition, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await repository.GetElements(condition, asNoTracking, cancellationToken,u=>u.Activities);
    }

    public async Task<User> GetUserById(Guid userId, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await repository.GetElementById(userId, asNoTracking, cancellationToken, u => u.Activities);
    }

    public async Task<IEnumerable<User>> GetUsers(bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await repository.GetElements(default!, asNoTracking, cancellationToken, u => u.Activities);
    }

    public async Task<bool> UpdateUser(UserDto dto, CancellationToken cancellationToken = default)
    {
        Guid id = dto.Id ?? throw new ArgumentNullException(nameof(dto.Id), "User ID cannot be null");
        User user = await GetUserById(id, false, cancellationToken);

        user.FirstName = dto.FirstName ?? user.FirstName;
        user.LastName = dto.LastName ?? user.LastName;
        user.Email = dto.Email ?? user.Email;
        user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;
        user.address = dto.Address ?? user.address;

        await repository.SaveChangesAsync(id, cancellationToken);
        return true;

    }
}




