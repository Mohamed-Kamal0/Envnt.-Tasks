using CatalogApi.Models;
// using MyApp.Helpers;
namespace CatalogApi.Services;

// In-memory catalog. Today's target is the search filter below: it works, it is
// inline, and it cannot be tested without standing the service up. See REFACTOR.md.
public class CatalogService : ICatalogService
{

    private static readonly List<Product> Products =
    [
        new Product { Id = 1, Name = "Mechanical Keyboard", Category = "Peripherals", Price = 89.99m, InStock = true },
        new Product { Id = 2, Name = "Wireless Mouse",      Category = "Peripherals", Price = 24.50m, InStock = true },
        new Product { Id = 3, Name = "27\" Monitor",        Category = "Displays",    Price = 219.00m, InStock = true },
        new Product { Id = 4, Name = "USB-C Hub",           Category = "Accessories", Price = 39.95m, InStock = true },
        new Product { Id = 5, Name = "Laptop Stand",        Category = "Accessories", Price = 32.00m, InStock = true },
        new Product { Id = 6, Name = "Webcam 1080p",        Category = "Peripherals", Price = 54.00m, InStock = false },
        new Product { Id = 7, Name = "Ultrawide Monitor",   Category = "Displays",    Price = 429.00m, InStock = true },
        new Product { Id = 8, Name = "Desk Mat",            Category = "Accessories", Price = 18.00m, InStock = true }
    ];

    public Task<IReadOnlyList<Product>> GetProductsAsync(string? search, string? sort)
    {
        var query = Products.AsEnumerable();

        // Search by name. NOTE: this is a CASE-SENSITIVE match — searching "mouse"
        // finds nothing, "Mouse" finds one. That is today's behavior, quirk included.
        // Changing it is a different ticket; a refactor that "fixes" it on the way
        // past is not a refactor.
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search, StringComparison.Ordinal));
        // query = ProductFilterHelper.FilterByName(query, search);
        query = sort switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            _ => query
        };

        IReadOnlyList<Product> result = query.ToList();
        return Task.FromResult(result);
    }
}
