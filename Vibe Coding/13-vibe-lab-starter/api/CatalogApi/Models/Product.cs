namespace CatalogApi.Models;

// The same Product you modelled in Week 1 — an in-memory entity, no database.
// Week 3 is not about the model; it's about how you work with an AI on code
// like this. Keep it boring on purpose.
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}
