using CatalogApi.Dtos;
using CatalogApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ICatalogService _catalog;
    private readonly IValidator<CreateProductRequest> _validator;

    public ProductsController(ICatalogService catalog, IValidator<CreateProductRequest> validator)
    {
        _catalog = catalog;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> Get([FromQuery] string? category, CancellationToken ct)
        => Ok(await _catalog.GetProductsAsync(category, ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id, CancellationToken ct)
    {
        var product = await _catalog.GetProductAsync(id, ct);
        return product is null ? NotFound() : Ok(product); // 404, not 200-with-null
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductRequest req, CancellationToken ct)
    {
        var validation = await _validator.ValidateAsync(req, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var created = await _catalog.CreateProductAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created); // 201 + Location
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _catalog.DeleteProductAsync(id, ct);
        return deleted ? NoContent() : NotFound(); // 204 or 404
    }
}
