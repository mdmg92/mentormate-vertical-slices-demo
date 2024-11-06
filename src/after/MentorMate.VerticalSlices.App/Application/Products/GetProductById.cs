using MediatR;
using MentorMate.VerticalSlices.App.Data;
using MentorMate.VerticalSlices.App.Entities;
using MentorMate.VerticalSlices.App.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MentorMate.VerticalSlices.App.Application.Products;

public class GetProductById : IEndpointDefinition
{
    public class Query : IRequest<Product?>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Product?>
    {
        private readonly ProductsDbContext _db;

        public Handler(ProductsDbContext db)
        {
            _db = db;
        }
        
        public Task<Product?> Handle(Query request, CancellationToken cancellationToken) 
            => _db.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/products/{id}", GetProductByIdEndpoint).WithOpenApi();
    }

    internal async Task<IResult> GetProductByIdEndpoint(IMediator mediator, int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new Query { Id = id }, cancellationToken);

        if (result is null)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(result);
    }
}