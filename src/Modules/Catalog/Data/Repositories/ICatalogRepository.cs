
namespace Catalog.Data.Repositories
{
    public interface ICatalogRepository
    {

        public Task<Product> CreateProduct(ProductDto productDto , CancellationToken cancellationToken);
        public Task<bool> UpdateProduct(ProductDto productDto , CancellationToken cancellationToken);
        public Task<bool> DeleteProduct(Guid productId , CancellationToken cancellationToken);
        public Task<Product> GetProductById(Guid productId ,bool AsNoTracking, CancellationToken cancellationToken);
        public Task<IEnumerable<Product>> GetProductByCondition(ProductDto condition, bool AsNoTracking, CancellationToken cancellationToken);
        public Task<IEnumerable<Product>> GetProducts(bool AsNoTracking, CancellationToken cancellationToken);
    }
}
