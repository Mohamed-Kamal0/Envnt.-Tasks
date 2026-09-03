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

    // TODO (you) — Day 3: the boundary rule — this still leaks Product ENTITIES out the door.
    // Change the return type to IReadOnlyList<ProductDto> and map with the ToDto helper below:
    //   (await _catalog.GetProductsAsync(inStock)).Select(ToDto).ToList()
    // [HttpGet]
    // public async Task<ActionResult<IReadOnlyList<ProductDto>>> Get([FromQuery] bool? inStock)
    //     => Ok((await _catalog.GetProductsAsync(inStock)).Select(ToDto).ToList());

    public class Body
    {
        public int Id
        {
            get;set;
        }
    };
    [HttpPost]
    public async Task<ActionResult<ProductDto>> GetById([FromBody] Body b)
    {
        Console.WriteLine(b.Id);
        // TODO (you) — Day 3: call GetProductAsync(id);
        //   null → NotFound()               (404 — never 200-with-null; returning null is a silent 204)
        //   found → Ok(ToDto(product))      (200)
        var product = await _catalog.GetProductAsync(b.Id);
        if (product == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(ToDto(product));
        }
    }

    // [HttpPost]
    // public async Task<ActionResult<ProductDto>> Create(CreateProductRequest req)
    // {
    //     // TODO (you) — Day 3: run the validator first — one gate, at the door:
    //     var validation = await _validator.ValidateAsync(req);
    //     if (!validation.IsValid)
    //     {
    //         return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
    //     }
    //     else
    //     {
    //         var product = await _catalog.CreateProductAsync(req);
    //         return CreatedAtAction(nameof(GetById), new { id = product.Id }, ToDto(product));
    //     }
    //     //   invalid → BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }))  (400)
    //     //   valid   → CreateProductAsync(req), then
    //     //             CreatedAtAction(nameof(GetById), new { id = product.Id }, ToDto(product))          (201 + Location header)
    // }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        // TODO (you) — Day 3: call DeleteProductAsync(id);
        //   true  → NoContent()   (204 — deleted, nothing to say)
        //   false → NotFound()    (404)
        bool deleted = await _catalog.DeleteProductAsync(id);
        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    // entity → DTO, at the boundary and nowhere else
    private static ProductDto ToDto(Product p) => new(p.Id, p.Name, p.Price, p.InStock);
}
