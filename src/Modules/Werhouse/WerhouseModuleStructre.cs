



using Shared.Data;


namespace Werhouse;

public class WerhouseModuleStructre : ModuleMechanism<WerhouseItem>
{

    // SubModels
    public interface History : IMModelConfiguration.Property;

    // Model configuration
    public interface IWerhouseItemConfigurations : IMModelConfiguration;


    // SubModel configuration
    public interface IWerhouseItemHistoryConfigurations : IMModelConfiguration.IMPropertyConfiguration<WerhouseItemHistory>;


    
    // DbContext
    public abstract class MWerhouseDbContext(DbContextOptions<WerhouseDbContext> options) : IMModelConfiguration.MContext<WerhouseDbContext>(options, new[] { typeof(WerhouseItem), typeof(WerhouseItemHistory) });


    //repositories
    public abstract class WerhouseRepository<R>(R repository) : IMModelConfiguration.LocalRepository<R, WerhouseDbContext>(repository) where R : class, IGenericRepository<WerhouseItem>;


    public class WerhouseSQLRepository(GenericDbContext<WerhouseDbContext> dbContext) :
        WerhouseLocalRepository<GenericRepository<WerhouseItem, WerhouseDbContext>>(
            new GenericRepository<WerhouseItem, WerhouseDbContext>(dbContext));


    public class CachedWerhouseRepository(WerhouseSQLRepository repository, IDistributedCache cache) :
        WerhouseLocalRepository<GenericCachedRepository<WerhouseItem, WerhouseDbContext>>(
            new GenericCachedRepository<WerhouseItem, WerhouseDbContext>(repository.getMasterRepository(), cache));



    //commands
    public interface GetNewItem : MPost<Guid, Guid>;
    public interface PerformOperation : MPost<WerhouseItemHistoryDto, Guid>;
    public interface SendItem : MPut<WerhouseItemDto, bool>;

    //queries

    public interface GetItemById : MGet<Guid, HashSet<WerhouseItemHistoryDto>>;
    public interface GetItemsByCondition : MGet<WerhouseItemDto,HashSet<WerhouseItemDto>>;
    



    
}


