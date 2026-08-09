namespace CatalogApi.Models;

// The model — for now a plain in-memory object. On day 4, EF Core maps this exact
// class to a database table.
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}
