using CatalogApi.Dtos;
using CatalogApi.Models;

namespace CatalogApi.Services;

// The interface is the seam. Controllers depend on THIS, not on CatalogService —
// on day 5 this seam is what lets tests swap the real database for a fake one.
public interface ICatalogService
{
    // Day 4: the filter grows up — a Category is a real entity now, so products are
    // filtered by category NAME (`?category=Books`) instead of the old `?inStock=` flag.
    Task<IReadOnlyList<Product>> GetProductsAsync(string? category);
    Task<Product?> GetProductAsync(int id);
    Task<Product> CreateProductAsync(CreateProductRequest req);
    Task<bool> DeleteProductAsync(int id);

    // This one already returns the DTO: counting a category's products belongs INSIDE the
    // query, not in the controller — a preview of the shape everything takes on day 5.
    Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync();
}
