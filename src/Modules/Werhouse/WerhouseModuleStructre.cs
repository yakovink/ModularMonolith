
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
    public abstract class MWerhouseDbContext(DbContextOptions<WerhouseDbContext> options) : 
        IMModelConfiguration.MContext<WerhouseDbContext>(
            options, new[] { typeof(WerhouseItem), typeof(WerhouseItemHistory) });


    //repositories
    public abstract class WerhouseRepository(IGenericRepository<WerhouseItem> repository) : IMModelConfiguration.LocalRepository<WerhouseDbContext>(repository);




    //commands
    public interface GetNewItem : MPost<Guid, Guid>;
    public interface PerformOperation : MPost<WerhouseItemHistoryDto, Guid>;
    public interface SendItem : MPut<WerhouseItemDto, bool>;

    //queries

    public interface GetItemById : MGet<Guid, HashSet<WerhouseItemHistoryDto>>;
    public interface GetItemsByCondition : MGet<WerhouseItemDto,HashSet<WerhouseItemDto>>;
    

    //controllers:
    public class WerhouseHttpController() : HttpController("http://localhost", 5000);

    
}


