using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Data;

// The gateway to the database. Every query and save goes through here.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // one DbSet per table
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // The one-to-many relationship, written out explicitly for clarity.
        // (EF Core would infer this from the conventions above, but naming it teaches it.)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}
