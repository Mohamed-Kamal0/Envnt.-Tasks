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
    private readonly IConfiguration _config;

    public ProductsController(ICatalogService catalog, IConfiguration config)
    {
        _catalog = catalog;
        _config  = config;
    }

    // GET /api/products?search=mouse&sort=price_asc&cheapOnly=true
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> Get(
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] bool cheapOnly = false)
    {
        var products = await _catalog.GetProductsAsync(search, sort, cheapOnly);
        return Ok(products.Select(ToDto).ToList());
    }

    // GET /api/products/featured — the banner at the top of the Angular page.
    [HttpGet("featured")]
    public async Task<ActionResult<ProductDto>> GetFeatured()
        => Ok(ToDto(await _catalog.GetFeaturedAsync()));

    // POST /api/products/sync — a pretend "push the catalog upstream" job.
    // The API key is read from config (User Secrets locally, env var in CI/prod).
    // It is never sent to the browser.
    [HttpPost("sync")]
    public IActionResult Sync()
    {
        var apiKey = _config["SyncApiKey"];
        // In a real implementation you would use apiKey here to call the upstream service:
        // await httpClient.PostAsync(upstreamUrl, ..., authHeader: $"Bearer {apiKey}");
        return Accepted();
    }

    // entity → DTO, at the boundary and nowhere else
    private static ProductDto ToDto(Product p) => new(p.Id, p.Name, p.Category, p.Price, p.InStock);
}
