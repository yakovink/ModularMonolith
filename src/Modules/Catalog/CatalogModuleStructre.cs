using System;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Mechanism;

namespace Catalog;

public class CatalogModuleStructre : ModuleMechanism<Product>
{


    // Model configuration
    public interface ICatalogConfigurations : MModelConfiguration<Product>;

    // DbContext
    public abstract class MCatalogDbContext(DbContextOptions<CatalogDbContext> options) : IMModelConfiguration.MContext<CatalogDbContext>(options, new[] { typeof(Product) });


    //repositories
    public interface IMCatalogRepository : IMModelConfiguration.IMRepository;

    //public abstract class MCatalogRepository(CatalogDbContext dbContext) : IMModelConfiguration.MRepository<CatalogDbContext>(dbContext);

    //public abstract class MCatalogCachedRepository(MCatalogRepository repository, IDistributedCache cache) : IMModelConfiguration.MCachedRepository<CatalogDbContext>(repository, cache);

    //commands
    public interface CreateProduct : MPost<ProductDto, Guid>;
    public interface UpdateProduct : MPut<ProductDto, bool>;
    public interface DeleteProduct : MDelete<Guid, bool>;
    //queries
    public interface GetProductById : MGet<Guid, ProductDto>;
    public interface GetProductsByCondition : MGet<ProductDto, HashSet<ProductDto>>;
    public interface GetProducts : MGet<PaginationRequest, PaginatedResult<ProductDto>>;
}
