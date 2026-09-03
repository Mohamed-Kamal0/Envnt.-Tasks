using CatalogApi.Models;

namespace CatalogApi.Services;

// The seam. The controller depends on THIS, never on CatalogService — same rule
// you learned in Week 1 day 5, and the reason a change here is easy to review.
public interface ICatalogService
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? search, string? sort, bool cheapOnly);

    // The product shown in the "Featured today" banner.
    Task<Product> GetFeaturedAsync();
}
