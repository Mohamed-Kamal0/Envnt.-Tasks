using CatalogApi.Dtos;
using CatalogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICatalogService _catalog;
    public CategoriesController(ICatalogService catalog) => _catalog = catalog;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> Get(CancellationToken ct)
        => Ok(await _catalog.GetCategoriesAsync(ct));
}
