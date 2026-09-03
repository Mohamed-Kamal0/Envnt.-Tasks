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

    // Wired for you — but it calls a service method YOU write today (GetCategoriesAsync).
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> Get()
        => Ok(await _catalog.GetCategoriesAsync());
}
