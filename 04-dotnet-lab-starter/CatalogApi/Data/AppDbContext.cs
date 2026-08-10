using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Data;

// The gateway to the database. Every query and save goes through here.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // one DbSet per table (given — the seeder needs them to compile; read them, they're
    // the properties your EF queries will start from)
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO (you) — Day 4: declare the one-to-many relationship explicitly:
        //   modelBuilder.Entity<Product>()
        //       .HasOne(<the product's category>)
        //       .WithMany(<the category's products>)
        //       .HasForeignKey(<the product's CategoryId>);
        // (EF Core could infer it from conventions, but naming it teaches it.)
        modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.products).HasForeignKey(p => p.CategoryId);
    }
}
