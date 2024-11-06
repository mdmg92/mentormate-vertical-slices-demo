using System.Reflection;
using MentorMate.VerticalSlices.App.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentorMate.VerticalSlices.App.Data;

public class ProductsDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}