using CatalogApi.Filtering;
using CatalogApi.Models;

namespace CatalogApi.Tests;

public class ProductFilterTests
{
    private static List<Product> Catalog() =>
    [
        new Product { Id = 1, Name = "Mechanical Keyboard", Category = "Peripherals", Price = 89.99m, InStock = true },
        new Product { Id = 2, Name = "Wireless Mouse", Category = "Peripherals", Price = 24.50m, InStock = true },
        new Product { Id = 3, Name = "27\" Monitor", Category = "Displays", Price = 219.00m, InStock = true },
        new Product { Id = 4, Name = "USB-C Hub", Category = "Accessories", Price = 39.95m, InStock = true },
        new Product { Id = 5, Name = "Laptop Stand", Category = "Accessories", Price = 32.00m, InStock = true },
        new Product { Id = 6, Name = "Webcam 1080p", Category = "Peripherals", Price = 54.00m, InStock = false },
        new Product { Id = 7, Name = "Ultrawide Monitor", Category = "Displays", Price = 429.00m, InStock = true },
        new Product { Id = 8, Name = "Desk Mat", Category = "Accessories", Price = 18.00m, InStock = true },
    ];

    [Fact]
    public void ByName_returns_everything_for_blank_and_null_search()
    {
        var products = Catalog();

        var blankResult = ProductFilter.ByName(products, "").Select(p => p.Id).ToList();
        var nullResult = ProductFilter.ByName(products, null).Select(p => p.Id).ToList();

        Assert.Equal(products.Select(p => p.Id), blankResult);
        Assert.Equal(products.Select(p => p.Id), nullResult);
    }

    [Fact]
    public void ByName_matches_one_product_for_case_sensitive_substring()
    {
        var result = ProductFilter.ByName(Catalog(), "Mouse").Select(p => p.Id).ToList();

        Assert.Equal(new[] { 2 }, result);
    }

    [Fact]
    public void ByName_returns_no_results_for_lowercase_query()
    {
        var result = ProductFilter.ByName(Catalog(), "mouse").Select(p => p.Id).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public void ByName_preserves_original_order_for_shared_substring()
    {
        var products = Catalog();

        var result = ProductFilter.ByName(products, "e").Select(p => p.Id).ToList();

        Assert.Equal(new[] { 1, 2, 6, 7, 8 }, result);
    }
}
