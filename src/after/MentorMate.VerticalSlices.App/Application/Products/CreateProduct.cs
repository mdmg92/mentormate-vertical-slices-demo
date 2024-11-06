using MediatR;
using MentorMate.VerticalSlices.App.Data;
using MentorMate.VerticalSlices.App.Entities;
using MentorMate.VerticalSlices.App.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MentorMate.VerticalSlices.App.Application.Products;

public class CreateProduct : IEndpointDefinition
{
    public class Command : IRequest<IResult>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Product MapToEntity() => new Product
        {
            Name = Name,
            Price = Price
        };
    }
    
    public class Handler : IRequestHandler<Command, IResult>
    {
        private readonly ProductsDbContext _db;

        public Handler(ProductsDbContext db) => _db = db;

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    throw new ArgumentException("The Name can't be empty");
                }

                if (request.Price <= 0)
                {
                    throw new ArgumentException("The price has to be a positive number above 0");
                }

                var product = request.MapToEntity();

                await _db.AddAsync(product, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
            
                return Results.Ok(product.Id);
            }
            catch (Exception ex)
            {
                return Results.BadRequest();
            }
        }
    }

    internal Task<IResult> CreateProductEndpoint(
        IMediator mediator, 
        [FromBody] Command request,
        CancellationToken cancellationToken)
        => mediator.Send(request, cancellationToken);

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/products", CreateProductEndpoint).WithOpenApi();
    }
}