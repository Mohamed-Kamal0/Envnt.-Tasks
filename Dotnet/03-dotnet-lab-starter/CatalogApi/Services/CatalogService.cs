using CatalogApi.Dtos;
using CatalogApi.Models;

namespace CatalogApi.Services;

// The business-logic layer — GIVEN today. Day 3 is about the REST shape (controller,
// DTOs, validation, status codes); the service's own logic is yesterday's and day 4-5's work.
public class CatalogService : ICatalogService
{
    // static: ONE list for the whole app, surviving across requests (each request gets
    // a fresh Scoped CatalogService, but they all share this list). It's a stand-in until
    // the real database arrives on day 4.
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Clean Code", Price = 32.00m, InStock = true },
        new Product { Id = 2, Name = "The Pragmatic Programmer", Price = 38.50m, InStock = true },
        new Product { Id = 3, Name = "USB-C Hub", Price = 24.99m, InStock = false }
    };

    public Task<IReadOnlyList<Product>> GetProductsAsync(bool? inStock)
    {
        var query = Products.AsEnumerable();

        // LINQ filter — only applied when stock status was asked for (day 2's work)
        if (inStock.HasValue)
            query = query.Where(p => p.InStock == inStock);

        IReadOnlyList<Product> result = query.OrderBy(p => p.Name).ToList();
        return Task.FromResult(result);
    }

    public Task<Product?> GetProductAsync(int id)
        => Task.FromResult(Products.FirstOrDefault(p => p.Id == id));

    public Task<Product> CreateProductAsync(CreateProductRequest req)
    {
        var product = new Product
        {
            Id = Products.Count == 0 ? 1 : Products.Max(p => p.Id) + 1, // the database does this on day 4
            Name = req.Name,
            Price = req.Price,
            InStock = req.InStock
        };
        Products.Add(product);
        return Task.FromResult(product);
    }

    public Task<bool> DeleteProductAsync(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return Task.FromResult(false);

        Products.Remove(product);
        return Task.FromResult(true);
    }
}
