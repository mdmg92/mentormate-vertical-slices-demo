using MentorMate.Traditional.Core.Entities;

namespace MentorMate.Traditional.Core.Interfaces;

public interface IProductsService
{
    Task<Product?> GetProductById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken);
    Task<int> CreateProduct(string name, decimal price, CancellationToken cancellationToken);
}