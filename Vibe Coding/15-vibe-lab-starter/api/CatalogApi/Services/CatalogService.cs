using CatalogApi.Models;

namespace CatalogApi.Services;

// In-memory catalog — no EF Core here on purpose. A static list keeps the bench
// runnable in one `dotnet run`, so the week's lesson stays "how do I work with
// the AI", not "why won't my migration apply".
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

    public Task<IReadOnlyList<Product>> GetProductsAsync(string? search, string? sort, decimal? maxPrice, decimal? minPrice)
    {
        var query = Products.AsEnumerable();

        // Search by name — the LINQ filter from Week 1 day 2.
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        // Sort, when the client asks for it. An unknown or missing value leaves the
        // order alone — a typo in a query string shouldn't break an endpoint.
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
