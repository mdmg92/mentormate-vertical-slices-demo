using MentorMate.Traditional.Core.Entities;

namespace MentorMate.Traditional.Core.Interfaces;

public interface IProductsRepository
{
    Task<Product?> GetProductById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken);
    Task<int> CreateProduct(Product product, CancellationToken cancellationToken);
}