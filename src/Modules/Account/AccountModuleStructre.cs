





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

    public abstract class AccountRepository(IGenericRepository<User> repository) : IMModelConfiguration.LocalRepository<AccountDbContext>(repository);




    //commands
    public interface CreateUser : MPost<UserDto, Guid>;
    public interface UpdateUser : MPut<UserDto, bool>;
    public interface DeleteUser : MDelete<Guid, bool>;
    public interface ChangePassword : MPost<PasswordDto, bool>;

    

    //queries
    public interface GetUserById : MGet<Guid, UserDto>;
    public interface GetUsersByCondition : MGet<UserDto, HashSet<UserDto>>;
    public interface GetUsers : MGet<PaginationRequest, PaginatedResult<UserDto>>;

    // controllers
    public class AccountHttpController() : HttpController("http://localhost",5000);
    



}
