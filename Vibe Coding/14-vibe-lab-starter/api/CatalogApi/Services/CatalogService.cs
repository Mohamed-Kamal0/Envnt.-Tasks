using CatalogApi.Models;

namespace CatalogApi.Services;

// In-memory catalog. This copy does NOT behave correctly — run it, read what the
// API console and the browser tell you, and fix what you find.
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

    public Task<IReadOnlyList<Product>> GetProductsAsync(string? search, string? sort, bool cheapOnly)
    {
        var query = Products.AsEnumerable();

        // Search by name.
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        // …and optionally keep only the budget picks (under $50).
        if (cheapOnly)
            query = query.Where(p => p.Price < 50);

        query = sort switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            _ => query
        };

        IReadOnlyList<Product> result = query.ToList();
        return Task.FromResult(result);
    }

    // The product for the "Featured today" banner.
    public Task<Product> GetFeaturedAsync()
        => Task.FromResult(Products.First(p => p.Id == 1));
}
