namespace CatalogApi.Models;

// A category groups products. One category has many products (one-to-many).
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Product> products { get; set; } = new();
    // TODO (you) — Day 4: the "many" side navigation property. One category holds the list
    // of its products; initialize it so it's never null:
    //   public List<Product> Products { get; set; } = new();
}
