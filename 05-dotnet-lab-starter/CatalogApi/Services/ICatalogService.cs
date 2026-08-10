using CatalogApi.Dtos;

namespace CatalogApi.Services;

// The interface is the seam. Controllers depend on THIS, not on CatalogService —
// which is what lets the tests swap in a service backed by an in-memory database.
public interface ICatalogService
{
    Task<IReadOnlyList<ProductDto>> GetProductsAsync(string? category, CancellationToken ct);
    Task<ProductDto?> GetProductAsync(int id, CancellationToken ct);
    Task<ProductDto> CreateProductAsync(CreateProductRequest req, CancellationToken ct);
    Task<bool> DeleteProductAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
}
