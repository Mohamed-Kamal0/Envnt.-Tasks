using CatalogApi.Models;

namespace CatalogApi.Services;

// The seam. The controller depends on THIS, never on CatalogService — same rule
// you learned in Week 1 day 5, and the reason an AI-written change here is easy
// to review: one interface, one implementation, no surprises.
public interface ICatalogService
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? search, string? sort, decimal? maxPrice, decimal? minPrice);
}
