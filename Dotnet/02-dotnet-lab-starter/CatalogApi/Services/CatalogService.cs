using CatalogApi.Models;

namespace CatalogApi.Services;

// The business-logic layer. It owns the queries; the controller just calls it.
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
        // TODO (you) — Day 2: when `inStock` HAS a value, filter with LINQ before ordering:
        //   Products.Where(p => p.InStock == inStock)
        // Careful: Where returns a NEW sequence — assign the result; LINQ never mutates the list.

        IReadOnlyList<Product> result = inStock != null ? Products.Where(p => p.InStock == inStock).OrderBy(p => p.Name).ToList()
        : Products.OrderBy(p => p.Name).ToList();

        // Task.FromResult because there's no real I/O yet — genuine `await` arrives with
        // the database on day 4. The signature is async-shaped on purpose, so day 4 only
        // changes the body, never the callers.
        return Task.FromResult(result);
    }


    //a.
    public Task<IReadOnlyList<Product>> GetProductsInStockAsync()
    {

        IReadOnlyList<Product> result = Products.Where(p => p.InStock == true).ToList();

        return Task.FromResult(result);
    }

    //b.
    public Task<IReadOnlyList<string>> GetProductsNamesAsync()
    {

        IReadOnlyList<string> result = Products.Select(p => p.Name).ToList();

        return Task.FromResult(result);
    }

    //c.
    public Task<IReadOnlyList<Product>> GetCheapProductsAsync()
    {

        IReadOnlyList<Product> result = Products.Where(p => p.Price < 50).ToList();

        return Task.FromResult(result);
    }

    //d.
    public Task<Product> GetFirstProductAsync()
    {

        Product result = Products.FirstOrDefault(p => p.InStock == true && p.Name == "Clean Code");

        return Task.FromResult(result);
    }

    //e.
    public Task<bool> GetExistExpensiveProductAsync()
    {

        bool result = Products.Any(p => p.Price > 100);

        return Task.FromResult(result);
    }

    //f.
    public Task<int> GetNumberOfElectronicsAsync()
    {

        int result = Products.Where(p => p.InStock == true && p.Name == "Electronics").ToList().Count;

        return Task.FromResult(result);
    }


    //g.
    public Task<IReadOnlyList<string>> GetBooksNamesAsync()
    {

        IReadOnlyList<string> result = Products.Where(p => p.Name == "Books").OrderBy(p => p.Name).Select(p => p.Name).ToList();

        return Task.FromResult(result);
    }
}
