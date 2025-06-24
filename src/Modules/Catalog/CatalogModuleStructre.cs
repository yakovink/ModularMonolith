
namespace Catalog;

public class CatalogModuleStructre : ModuleMechanism<Product>
{


    // Model configuration
    public interface ICatalogConfigurations : MModelConfiguration<Product>;

    // DbContext
    public abstract class MCatalogDbContext(DbContextOptions<CatalogDbContext> options) : IMModelConfiguration.MContext<CatalogDbContext>(options, new[] { typeof(Product) });


    //repositories
    public abstract class CatalogRepository(IGenericRepository<Product> repository) : IMModelConfiguration.LocalRepository<CatalogDbContext>(repository) ;



    //commands
    public interface CreateProduct : MPost<ProductDto, Guid>;
    public interface UpdateProduct : MPut<ProductDto, bool>;
    public interface DeleteProduct : MDelete<Guid, bool>;
    //queries
    public interface GetProductById : MGet<Guid, ProductDto>;
    public interface GetProductsByCondition : MGet<ProductDto, HashSet<ProductDto>>;
    public interface GetProducts : MGet<PaginationRequest, PaginatedResult<ProductDto>>;

    //controllers

    public class CatalogHttpController() : HttpController("http://localhost", 5000);
}
