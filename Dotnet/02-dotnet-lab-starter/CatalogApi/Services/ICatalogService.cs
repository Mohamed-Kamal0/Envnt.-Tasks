using CatalogApi.Models;

namespace CatalogApi.Services;

// The interface is the seam. Controllers depend on THIS, not on CatalogService —
// on day 5 this seam is what lets tests swap the real database for a fake one.
public interface ICatalogService
{
    Task<IReadOnlyList<Product>> GetProductsAsync(bool? inStock);
    Task<IReadOnlyList<Product>> GetProductsInStockAsync();
    Task<IReadOnlyList<string>> GetProductsNamesAsync();
    Task<IReadOnlyList<Product>> GetCheapProductsAsync();
    Task<Product> GetFirstProductAsync();
    Task<bool> GetExistExpensiveProductAsync();
    Task<int> GetNumberOfElectronicsAsync();
    Task<IReadOnlyList<string>> GetBooksNamesAsync();

}
