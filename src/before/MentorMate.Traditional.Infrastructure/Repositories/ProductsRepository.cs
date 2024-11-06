using MentorMate.Traditional.Core.Data;
using MentorMate.Traditional.Core.Entities;
using MentorMate.Traditional.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorMate.Traditional.Core.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly ProductsDbContext _db;

    public ProductsRepository(ProductsDbContext db)
    {
        _db = db;
    }

    public Task<Product?> GetProductById(int id, CancellationToken cancellationToken) 
        => _db.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken) 
        => await _db.Products.ToListAsync(cancellationToken);

    public async Task<int> CreateProduct(Product product, CancellationToken cancellationToken)
    {
        _db.Add(product);

        await _db.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}