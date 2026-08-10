using CatalogApi.Models;

namespace CatalogApi.Data;

// Seeds a few rows so the API has something to show on first run.
// Kept OUT of OnModelCreating on purpose: HasData would also seed the in-memory
// database the tests use, and the tests want to start empty.
public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Categories.Any()) return; // already seeded

        var books = new Category { Name = "Books" };
        var tech = new Category { Name = "Electronics" };
        db.Categories.AddRange(books, tech);

        db.Products.AddRange(
            new Product { Name = "Clean Code", Price = 32.00m, InStock = true, Category = books },
            new Product { Name = "The Pragmatic Programmer", Price = 38.50m, InStock = true, Category = books },
            new Product { Name = "USB-C Hub", Price = 24.99m, InStock = false, Category = tech });

        db.SaveChanges();
    }
}
