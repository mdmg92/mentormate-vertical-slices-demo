using MentorMate.Traditional.App.Models;
using MentorMate.Traditional.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MentorMate.Traditional.App.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductsService productsService, ILogger<ProductsController> logger)
    {
        _productsService = productsService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting product with id: {Id}", 0);

        var product = await _productsService.GetProductById(id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all products");

        var products = await _productsService.GetAllProducts(cancellationToken);

        if (!products.Any())
        {
            return NoContent();
        }
        
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto product, CancellationToken cancellationToken)
    {
        var result = await _productsService.CreateProduct(product.Name, product.Price, cancellationToken);
        
        return Ok(result);
    }
}