
namespace Catalog.Data.Repositories
{
    public class CatalogLocalRepository(IGenericRepository<Product> repository, IHttpController controller) : CatalogModuleStructre.CatalogRepository(repository), ICatalogRepository
    {
        public async Task<Product> CreateProduct(ProductDto productDto, CancellationToken cancellationToken)
        {
            Product product = Product.Create(
            productDto.Name??throw new ArgumentException("name cannot be null"),
            productDto.Categories?.Select(category => Enum.Parse<ProductCategory>(category)).ToList() ?? new List<ProductCategory>(),
            productDto.Price ?? throw new ArgumentException("price cannot be null"),
            productDto.Description ?? string.Empty,
            productDto.ImageUrl ?? string.Empty
            );
            return await repository.CreateElement(product, cancellationToken);
        }

        public async Task<bool> DeleteProduct(Guid productId, CancellationToken cancellationToken)
        {
            return await repository.DeleteElement(productId, cancellationToken);    
        }

        public async Task<IEnumerable<Product>> GetProductByCondition(ProductDto condition, bool AsNoTracking, CancellationToken cancellationToken)
        {
            Expression<Func<Product, bool>> filter = i =>
                (i.Id == condition.Id || condition.Id == null) &&
                (i.Price == condition.Price || condition.Price == null) &&
                (condition.Name == null || i.Name.Contains(condition.Name)) &&
                (condition.Description == null || i.Description.Contains(condition.Description))&&
                (condition.ImageUrl==null || condition.ImageUrl==i.ImageFile);
            return await repository.GetElements(filter, AsNoTracking, cancellationToken);
        }

        public async Task<Product> GetProductById(Guid productId, bool AsNoTracking, CancellationToken cancellationToken)
        {
            return await repository.GetElementById(productId,AsNoTracking, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProducts(bool AsNoTracking, CancellationToken cancellationToken)
        {
            return await repository.GetElements(default!, AsNoTracking, cancellationToken);
        }

        public async  Task<bool> UpdateProduct(ProductDto productDto, CancellationToken cancellationToken)
        {
            Guid productId = productDto.Id?? throw new ArgumentNullException(nameof(productId));
            Product product = await GetProductById(productId, false, cancellationToken);

            product.Price = productDto.Price ?? product.Price;
            product.Name = productDto.Name ?? product.Name;
            product.Description = productDto.Description ?? product.Description;
            product.ImageFile = productDto.ImageUrl ?? product.ImageFile;
            product.Categories = productDto.Categories?.Select(i=> Enum.Parse<ProductCategory>(i)).ToList()?? product.Categories;
            await repository.SaveChangesAsync(productId, cancellationToken);
            return true;

            
        }
    }
}
