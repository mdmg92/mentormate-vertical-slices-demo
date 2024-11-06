using MentorMate.Traditional.Core.Interfaces;
using MentorMate.Traditional.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MentorMate.Traditional.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductsService, ProductsService>();

        return services;
    }
}