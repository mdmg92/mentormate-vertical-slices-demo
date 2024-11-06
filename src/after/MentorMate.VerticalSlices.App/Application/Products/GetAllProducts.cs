using MediatR;
using MentorMate.VerticalSlices.App.Data;
using MentorMate.VerticalSlices.App.Entities;
using MentorMate.VerticalSlices.App.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MentorMate.VerticalSlices.App.Application.Products;

public class GetAllProducts : IEndpointDefinition
{
    public class Query : IRequest<IEnumerable<Product>>
    {
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Product>>
    {
        private readonly ProductsDbContext _db;

        public Handler(ProductsDbContext db) => _db = db;

        public async Task<IEnumerable<Product>> Handle(Query request, CancellationToken cancellationToken) 
            => await _db.Products.ToListAsync(cancellationToken);
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/products", EndpointHandler).WithOpenApi();
    }

    internal async Task<IResult> EndpointHandler(IMediator mediator, CancellationToken cancellationToken)
    {
        var results = await mediator.Send(new Query(), cancellationToken);

        if (!results.Any())
        {
            return Results.NoContent();
        }
        
        return Results.Ok(results);
    }
}