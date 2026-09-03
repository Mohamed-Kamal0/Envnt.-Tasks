using CatalogApi.Data;
using CatalogApi.Dtos;
using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Services;

// The business-logic layer. Today it stops pretending:
// TODO (you) — Day 4: retire the static lists — inject AppDbContext through the
// constructor and rewrite each method as a real EF Core query:
//

// The per-method hints are below. Delete the lists once nothing references them.
public class CatalogService : ICatalogService
{
    private readonly AppDbContext _db;
    public CatalogService(AppDbContext db) => _db = db;

    // The stand-in "database" — replaced by AppDbContext today.
    private static readonly Category Books = new() { Id = 1, Name = "Books" };
    private static readonly Category Electronics = new() { Id = 2, Name = "Electronics" };
    private static readonly List<Category> Categories = new() { Books, Electronics };
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Clean Code", Price = 32.00m, InStock = true, CategoryId = 1, Category = Books },
        new Product { Id = 2, Name = "The Pragmatic Programmer", Price = 38.50m, InStock = true, CategoryId = 1, Category = Books },
        new Product { Id = 3, Name = "USB-C Hub", Price = 24.99m, InStock = false, CategoryId = 2, Category = Electronics }
    };

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? category)
    {
        // TODO (you) — Day 4: EF version — start from _db.Products.Include(p => p.Category)
        // (Include loads the related Category in the same query), keep the same
        // filter-when-asked, then OrderBy(p => p.Name) and await ToListAsync().
        var query = _db.Products.Include(p => p.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category!.Name == category);

        return await query.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        // TODO (you) — Day 4: EF version — _db.Products.Include(p => p.Category)
        //   .FirstOrDefaultAsync(p => p.Id == id)
        return await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> CreateProductAsync(CreateProductRequest req)
    {
        // TODO (you) — Day 4: EF version — new Product from req (NO Id: the database assigns it),
        // _db.Products.Add(product), await _db.SaveChangesAsync(), then load the Category so the
        // controller can put a name in the response DTO:
        //   product.Category = await _db.Categories.FindAsync(product.CategoryId);
        var product = new Product
        {
            Id = Products.Count == 0 ? 1 : Products.Max(p => p.Id) + 1,
            Name = req.Name,
            Price = req.Price,
            InStock = req.InStock,
            CategoryId = req.CategoryId,
            Category = Categories.FirstOrDefault(c => c.Id == req.CategoryId)
        };
        product.Category = await _db.Categories.FindAsync(product.CategoryId);
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        // TODO (you) — Day 4: EF version — await _db.Products.FindAsync(id); null → false;
        // otherwise _db.Products.Remove(product), await SaveChangesAsync(), true.
        var product = await _db.Products.FindAsync(id);
        if (product is null) return false;

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync()
    {
        // TODO (you) — Day 4: EF version — _db.Categories.OrderBy(c => c.Name)
        //   .Select(c => new CategoryDto(c.Id, c.Name, c.Products.Count))  ← needs your navigation property
        //   .ToListAsync()
        return await _db.Categories.OrderBy(c => c.Name).Include(c => c.products).Select(c => new CategoryDto(c.Id, c.Name, c.products.Count)).ToListAsync();
        // throw new NotImplementedException();
    }
}
