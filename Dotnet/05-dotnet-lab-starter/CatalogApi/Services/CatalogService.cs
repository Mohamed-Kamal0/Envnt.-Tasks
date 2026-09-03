using CatalogApi.Data;
using CatalogApi.Dtos;
using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Services;

// The business-logic layer. It owns the LINQ + EF queries; the controller just calls it.
//
// GetProductsAsync is IMPLEMENTED FOR YOU — it is the worked example. Read it line by line,
// then write the remaining methods in the same shape. (Add the `async` keyword back to
// each method as you implement it.)
public class CatalogService : ICatalogService
{
    private readonly AppDbContext _db;
    public CatalogService(AppDbContext db) => _db = db;

    // The worked example: Include the Category, filter only when asked, project to a DTO.
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
        // TODO (you) — Day 5: Include the Category, Where(p => p.Id == id), Select to ProductDto, FirstOrDefaultAsync(ct).
        return await _db.Products.Where(p => p.Id == id).Select(p => new ProductDto(p.Id, p.Name, p.Price, p.InStock, p.Category!.Name)).FirstOrDefaultAsync(ct);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductRequest req, CancellationToken ct)
    {
        // TODO (you) — Day 5: new Product from req, Add + SaveChangesAsync, then look up the category's name and return the ProductDto.
        var product = new Product
        {
            Id = _db.Products.Count() == 0 ? 1 : _db.Products.Max(p => p.Id) + 1,
            Name = req.Name,
            Price = req.Price,
            InStock = req.InStock,
            CategoryId = req.CategoryId,
            Category = _db.Categories.FirstOrDefault(c => c.Id == req.CategoryId)
        };
        _db.Add(product);
        await _db.SaveChangesAsync();
        return new ProductDto(product.Id, product.Name, product.Price, product.InStock, product.Category!.Name);
    }

    public async Task<bool> DeleteProductAsync(int id, CancellationToken ct)
    {
        // TODO (you) — Day 5: FindAsync the product; null → false; otherwise Remove + SaveChangesAsync and return true.
        // throw new NotImplementedException();
        var product = await _db.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken ct)
    {
        // TODO (you) — Day 5: OrderBy(c => c.Name), Select to CategoryDto (ProductCount = c.Products.Count), ToListAsync(ct).
        return await _db.Categories.Include(c => c.Products).Select(c => new CategoryDto(c.Id, c.Name, ProductCount: c.Products.Count)).ToListAsync(ct);
    }
}
