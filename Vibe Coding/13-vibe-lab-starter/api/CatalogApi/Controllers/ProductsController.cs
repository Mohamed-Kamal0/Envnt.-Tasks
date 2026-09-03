using CatalogApi.Dtos;
using CatalogApi.Models;
using CatalogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ICatalogService _catalog;

    public ProductsController(ICatalogService catalog) => _catalog = catalog;

    // GET /api/products?search=mouse&sort=price_asc
    // Thin controller: it takes the query string, calls the service, maps to DTOs.
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> Get(
        [FromQuery] string? search,
        [FromQuery] string? sort)
    {
        var products = await _catalog.GetProductsAsync(search, sort);
        return Ok(products.Select(ToDto).ToList());
    }

    // entity → DTO, at the boundary and nowhere else
    private static ProductDto ToDto(Product p) => new(p.Id, p.Name, p.Category, p.Price, p.InStock);
}
