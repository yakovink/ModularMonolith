





namespace Account;

public class AccountModuleStructre : ModuleMechanism<User>
{

    //SubModel
    public interface Activity : IMModelConfiguration.Property;

    // submodel configuration

    public interface IActivityConfigurations : IMModelConfiguration.IMPropertyConfiguration<UserActivity>;



    // Model configuration
    public interface IAccountConfigurations : MModelConfiguration<User>;

    // DbContext
    public abstract class MAccountDbContext(DbContextOptions<AccountDbContext> options) : IMModelConfiguration.MContext<AccountDbContext>(options, new[] { typeof(User) });


    //repositories

    public abstract class AccountRepository<R>(R repository) : IMModelConfiguration.LocalRepository<R,AccountDbContext>(repository) where R : class , IGenericRepository<User>;


    public class AccountSQLRepository(GenericDbContext<AccountDbContext> dbContext) :
        AccountLocalRepository<GenericRepository<User, AccountDbContext>>(
            new GenericRepository<User, AccountDbContext>(dbContext));


    public class CachedAccountRepository(AccountSQLRepository repository, IDistributedCache cache) :
        AccountLocalRepository<GenericCachedRepository<User, AccountDbContext>>(
            new GenericCachedRepository<User, AccountDbContext>(repository.getMasterRepository(), cache));


    //commands
    public interface CreateUser : MPost<UserDto, Guid>;
    public interface UpdateUser : MPut<UserDto, bool>;
    public interface DeleteUser : MDelete<Guid, bool>;
    public interface ChangePassword : MPost<PasswordDto, bool>;

    public 

    //queries
    public interface GetUserById : MGet<Guid, UserDto>;
    public interface GetUsersByCondition : MGet<UserDto, HashSet<UserDto>>;
    public interface GetUsers : MGet<PaginationRequest, PaginatedResult<UserDto>>;

    



}
