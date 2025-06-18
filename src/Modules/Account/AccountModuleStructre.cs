using System;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Mechanism;

namespace Account;

public class AccountModuleStructre : ModuleMechanism<User>
{



    // Model configuration
    public interface IAccountConfigurations : MModelConfiguration<User>;

    // DbContext
    public abstract class MAccountDbContext(DbContextOptions<AccountDbContext> options) : IMModelConfiguration.MContext<AccountDbContext>(options, new[] { typeof(User) });


    //repositories
    public interface IMAccountRepository : IMModelConfiguration.IMRepository;

    public abstract class MAccountRepository(AccountDbContext dbContext) : IMModelConfiguration.MRepository<AccountDbContext>(dbContext);

    public abstract class MAccountCachedRepository(MAccountRepository repository, IDistributedCache cache) : IMModelConfiguration.MCachedRepository<AccountDbContext>(repository, cache);


    //commands
    public interface CreateUser : MPost<UserDto, Guid>;
    public interface UpdateUser : MPut<UserDto, bool>;
    public interface DeleteUser : MDelete<Guid, bool>;
    public interface ChangePassword : MPost<PasswordDto, bool>;

    //queries
    public interface GetUserById : MGet<Guid, UserDto>;
    public interface GetUsersByCondition : MGet<UserDto, HashSet<UserDto>>;
    public interface GetUsers : MGet<PaginationRequest, PaginatedResult<UserDto>>;

    



}
