using CatalogApi.Dtos;
using CatalogApi.Models;
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

    // The boundary rule: DTOs cross the door, entities never do.
    // Day 4: the filter is now ?category=<name>.
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> Get([FromQuery] string? category)
        => Ok((await _catalog.GetProductsAsync(category)).Select(ToDto).ToList());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _catalog.GetProductAsync(id);
        return product is null ? NotFound() : Ok(ToDto(product)); // 404, not 200-with-null
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductRequest req)
    {
        // one gate, at the door — the validator decides, the controller just reports
        var validation = await _validator.ValidateAsync(req);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var product = await _catalog.CreateProductAsync(req);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ToDto(product)); // 201 + Location
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _catalog.DeleteProductAsync(id);
        return deleted ? NoContent() : NotFound(); // 204 or 404
    }

    // entity → DTO, at the boundary and nowhere else (the DTO carries a flat category name)
    private static ProductDto ToDto(Product p) => new(p.Id, p.Name, p.Price, p.InStock, p.Category?.Name ?? "");
}
