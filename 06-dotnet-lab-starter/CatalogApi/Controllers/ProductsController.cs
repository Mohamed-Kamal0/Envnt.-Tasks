using CatalogApi.Dtos;
using CatalogApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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

    // Reads are public — anyone can browse the catalog. No attribute needed here.
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> Get([FromQuery] string? category, CancellationToken ct)
        => Ok(await _catalog.GetProductsAsync(category, ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id, CancellationToken ct)
    {
        var product = await _catalog.GetProductAsync(id, ct);
        return product is null ? NotFound() : Ok(product); // 404, not 200-with-null
    }

    // TODO (you) — Day 6: protect writes — a POST without a valid token must return 401.
    // One attribute does it (the using is already at the top of the file).
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductRequest req, CancellationToken ct)
    {
        var validation = await _validator.ValidateAsync(req, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var created = await _catalog.CreateProductAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created); // 201 + Location
    }

    // Editing needs a token, like creating. 404 when the id does not exist —
    // never a silent 200 on a product that was never there.
    // TODO (you) — Day 6: this one is [Authorize] too.
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductDto>> Update(int id, UpdateProductRequest req, CancellationToken ct)
    {
        var updated = await _catalog.UpdateProductAsync(id, req, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    // TODO (you) — Day 6: Admin for delete — a token WITHOUT the Admin role must return 403,
    // no token at all must return 401. The attribute takes a Roles argument.
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _catalog.DeleteProductAsync(id, ct);
        return deleted ? NoContent() : NotFound(); // 204 or 404
    }
}
