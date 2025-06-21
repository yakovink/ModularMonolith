using Catalog.Data.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Data;
using Shared.Mechanism;
using System;

namespace Catalog;

public class CatalogModuleStructre : ModuleMechanism<Product>
{


    // Model configuration
    public interface ICatalogConfigurations : MModelConfiguration<Product>;

    // DbContext
    public abstract class MCatalogDbContext(DbContextOptions<CatalogDbContext> options) : IMModelConfiguration.MContext<CatalogDbContext>(options, new[] { typeof(Product) });


    //repositories
    public abstract class CatalogRepository<R>(R repository) : IMModelConfiguration.LocalRepository<R, CatalogDbContext>(repository) where R : class, IGenericRepository<Product>;


    public class CatalogSQLRepository(GenericDbContext<CatalogDbContext> dbContext) :
        CatalogLocalRepository<GenericRepository<Product, CatalogDbContext>>(
            new GenericRepository<Product, CatalogDbContext>(dbContext));


    public class CachedCatalogRepository(CatalogSQLRepository repository, IDistributedCache cache) :
        CatalogLocalRepository<GenericCachedRepository<Product, CatalogDbContext>>(
            new GenericCachedRepository<Product, CatalogDbContext>(repository.getMasterRepository(), cache));

    //commands
    public interface CreateProduct : MPost<ProductDto, Guid>;
    public interface UpdateProduct : MPut<ProductDto, bool>;
    public interface DeleteProduct : MDelete<Guid, bool>;
    //queries
    public interface GetProductById : MGet<Guid, ProductDto>;
    public interface GetProductsByCondition : MGet<ProductDto, HashSet<ProductDto>>;
    public interface GetProducts : MGet<PaginationRequest, PaginatedResult<ProductDto>>;
}
