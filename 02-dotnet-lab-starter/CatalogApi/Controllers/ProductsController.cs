using CatalogApi.Models;
using CatalogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers;

[ApiController]                 // automatic 400s for bad input, among other things
[Route("api/[controller]")]     // [controller] = "Products" → /api/products
public class ProductsController : ControllerBase
{
    private readonly ICatalogService _catalog;

    // The service ARRIVES through the constructor — the DI container news it up, not us.
    public ProductsController(ICatalogService catalog) => _catalog = catalog;



    // TODO (you) — Day 2: accept an optional query parameter and pass it through:
    //   Get([FromQuery] bool? inStock)  →  GetProductsAsync(inStock)
    // Then try /api/products?inStock=banana and watch [ApiController] turn it into a 400.
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsAsync([FromQuery] bool? inStock)
        => Ok(await _catalog.GetProductsAsync(inStock));


    //a.
    [HttpGet("instock")]
    // [Route("/instock")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsInStockAsync()
        => Ok(await _catalog.GetProductsInStockAsync());

    //b.
    [HttpGet("names")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductsNamesAsync()
        => Ok(await _catalog.GetProductsNamesAsync());
    //c.
    [HttpGet("cheap")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetCheapProductsAsync()
        => Ok(await _catalog.GetCheapProductsAsync());
    //d.
    [HttpGet("first")]
    public async Task<ActionResult<Product>> GetFirstProductAsync()
        => Ok(await _catalog.GetFirstProductAsync());
    //e.
    [HttpGet("expensive")]
    public async Task<ActionResult<bool>> GetExistExpensiveProductAsync()
        => Ok(await _catalog.GetExistExpensiveProductAsync());
    //f.
    [HttpGet("electronics")]
    public async Task<ActionResult<int>> GetNumberOfElectronicsAsync()
        => Ok(await _catalog.GetNumberOfElectronicsAsync());
    //g.
    [HttpGet("books")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBooksNamesAsync()
        => Ok(await _catalog.GetBooksNamesAsync());






}
