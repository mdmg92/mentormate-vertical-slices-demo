using MentorMate.Traditional.Core.Entities;
using MentorMate.Traditional.Core.Interfaces;

namespace MentorMate.Traditional.Core.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public Task<Product?> GetProductById(int id, CancellationToken cancellationToken)
        => _productsRepository.GetProductById(id, cancellationToken);

    public Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken) 
        => _productsRepository.GetAllProducts(cancellationToken);

    public async Task<int> CreateProduct(string name, decimal price, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "The Name can't be empty");
        }

        if (price <= 0)
        {
            throw new ArgumentException("The price has to be a positive number above 0", nameof(price));
        }

        var product = new Product
        {
            Name = name,
            Price = price
        };

        var id = await _productsRepository.CreateProduct(product, cancellationToken);

        return id;
    }
}