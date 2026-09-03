using CatalogApi.Models;

namespace CatalogApi.Tests;

// One trivial test so `dotnet test` is green BEFORE you start. Today's refactor
// is guarded: you add the real tests here (ProductFilterTests) first, watch them
// describe the current behavior, and only then move the code.
public class SmokeTests
{
    [Fact]
    public void Test_project_is_wired_up()
    {
        var product = new Product { Id = 1, Name = "Mechanical Keyboard", Category = "Peripherals", Price = 89.99m, InStock = true };

        Assert.Equal("Mechanical Keyboard", product.Name);
    }
}
