namespace CatalogApi.Models;

// A category groups products. One category has many products (one-to-many).
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    // navigation property — the "many" side of the relationship
    public List<Product> Products { get; set; } = new();
}
