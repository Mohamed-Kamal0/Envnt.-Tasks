namespace CatalogApi.Models;

// An entity — one row in the Products table. EF Core maps it to the database.
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public bool InStock { get; set; }

    // Day 4: foreign key + navigation property — the "one" side of the relationship.
    // (Given, so the seeder compiles; the Category side and the wiring are yours.)
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
