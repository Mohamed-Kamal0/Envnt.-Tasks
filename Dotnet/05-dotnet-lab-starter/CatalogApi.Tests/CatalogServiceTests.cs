using CatalogApi.Data;
using CatalogApi.Dtos;
using CatalogApi.Models;
using CatalogApi.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CatalogApi.Tests;

// Day 5: proving the service works — the reason the service layer sits behind an interface.
// Each test gets a FRESH in-memory database (no SQLite file, no shared state), so the
// interface is the seam that lets us swap the real database for a fake one.
public class CatalogServiceTests
{
    private static AppDbContext NewDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetProducts_filters_by_category()
    {
        // Arrange
        using var db = NewDb();
        db.Categories.AddRange(
            new Category { Id = 1, Name = "Books" },
            new Category { Id = 2, Name = "Electronics" });
        db.Products.AddRange(
            new Product { Name = "Clean Code", Price = 32m, InStock = true, CategoryId = 1 },
            new Product { Name = "USB-C Hub", Price = 25m, InStock = true, CategoryId = 2 });
        await db.SaveChangesAsync();
        var service = new CatalogService(db);

        // Act
        var result = await service.GetProductsAsync("Books", CancellationToken.None);

        // Assert
        var only = Assert.Single(result);
        Assert.Equal("Clean Code", only.Name);
        Assert.Equal("Books", only.Category);
    }

    [Fact]
    public async Task CreateProduct_persists_and_returns_dto()
    {
        using var db = NewDb();
        db.Categories.Add(new Category { Id = 1, Name = "Books" });
        await db.SaveChangesAsync();
        var service = new CatalogService(db);

        var created = await service.CreateProductAsync(
            new CreateProductRequest("Refactoring", 40m, true, 1), CancellationToken.None);

        Assert.True(created.Id > 0);
        Assert.Equal("Books", created.Category);
        Assert.Equal(1, await db.Products.CountAsync());
    }

    [Fact]
    public async Task GetProduct_returns_null_for_a_missing_id()
    {
        using var db = NewDb();
        var service = new CatalogService(db);

        var found = await service.GetProductAsync(999, CancellationToken.None);

        Assert.Null(found);
    }

    [Fact]
    public async Task DeleteProduct_removes_the_row_and_reports_true()
    {
        using var db = NewDb();
        db.Categories.Add(new Category { Id = 1, Name = "Books" });
        db.Products.Add(new Product { Id = 5, Name = "Old", Price = 1m, CategoryId = 1 });
        await db.SaveChangesAsync();
        var service = new CatalogService(db);

        var deleted = await service.DeleteProductAsync(5, CancellationToken.None);

        Assert.True(deleted);
        Assert.Equal(0, await db.Products.CountAsync());
    }
}
