
namespace Account.Data.Repositories
{
    public interface IAccountRepository : AccountModuleStructre.IMModelConfiguration.IMRepository
    {
        public Task<User> CreateUser(UserDto userDto, CancellationToken cancellationToken = default);
        public Task<bool> UpdateUser(UserDto dto, CancellationToken cancellationToken = default);
        public Task<bool> ChangePassword(PasswordDto passDto, CancellationToken cancellationToken = default);
        public Task<User> GetUserById(Guid userId, bool asNoTracking = true, CancellationToken cancellationToken = default);
        public Task<IEnumerable<User>> GetUserByCondition(Expression<Func<User, bool>> condition, bool asNoTracking = true, CancellationToken cancellationToken = default);
        public Task<IEnumerable<User>> GetUsers(bool asNoTracking = true, CancellationToken cancellationToken = default);

        
    }
}
