using CatalogApi.Data;
using CatalogApi.Dtos;
using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Services;

// The business-logic layer. It owns the LINQ + EF queries; the controller just calls it.
public class CatalogService : ICatalogService
{
    private readonly AppDbContext _db;
    public CatalogService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(string? category, CancellationToken ct)
    {
        var query = _db.Products.Include(p => p.Category).AsQueryable();

        // LINQ filter — only applied when a category was asked for
        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category!.Name == category);

        return await query
            .OrderBy(p => p.Name)
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.InStock, p.Category!.Name))
            .ToListAsync(ct);
    }

    public async Task<ProductDto?> GetProductAsync(int id, CancellationToken ct)
    {
        return await _db.Products
            .Include(p => p.Category)
            .Where(p => p.Id == id)
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.InStock, p.Category!.Name))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductRequest req, CancellationToken ct)
    {
        var product = new Product
        {
            Name = req.Name,
            Price = req.Price,
            InStock = req.InStock,
            CategoryId = req.CategoryId
        };
        _db.Products.Add(product);
        await _db.SaveChangesAsync(ct);

        var categoryName = await _db.Categories
            .Where(c => c.Id == product.CategoryId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync(ct) ?? "";

        return new ProductDto(product.Id, product.Name, product.Price, product.InStock, categoryName);
    }

    // Load, copy the allowed fields, save. Only these four move — the client
    // cannot smuggle in an Id or anything else it is not allowed to change.
    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductRequest req, CancellationToken ct)
    {
        var product = await _db.Products.FindAsync(new object[] { id }, ct);
        if (product is null) return null;

        product.Name = req.Name;
        product.Price = req.Price;
        product.InStock = req.InStock;
        product.CategoryId = req.CategoryId;
        await _db.SaveChangesAsync(ct);

        var categoryName = await _db.Categories
            .Where(c => c.Id == product.CategoryId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync(ct) ?? "";

        return new ProductDto(product.Id, product.Name, product.Price, product.InStock, categoryName);
    }

    public async Task<bool> DeleteProductAsync(int id, CancellationToken ct)
    {
        var product = await _db.Products.FindAsync(new object[] { id }, ct);
        if (product is null) return false;

        _db.Products.Remove(product);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken ct)
    {
        return await _db.Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto(c.Id, c.Name, c.Products.Count))
            .ToListAsync(ct);
    }
}
