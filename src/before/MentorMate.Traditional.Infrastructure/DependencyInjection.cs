using MentorMate.Traditional.Core.Data;
using MentorMate.Traditional.Core.Interfaces;
using MentorMate.Traditional.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MentorMate.Traditional.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddDbContext<ProductsDbContext>(options =>
            options.UseInMemoryDatabase("demo"));
        return services;
    }
}