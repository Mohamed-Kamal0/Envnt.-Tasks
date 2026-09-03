namespace CatalogApi.Dtos;

// DTOs — Data Transfer Objects. The boundary rule: entities never cross the door.
// A DTO decides exactly what goes in and out — no navigation loops, no over-posting.

// What the API returns for a product (note: a flat category NAME, not the whole Category entity).
public record ProductDto(int Id, string Name, decimal Price, bool InStock, string Category);

// What the API accepts to create a product (no Id — the database assigns it).
public record CreateProductRequest(string Name, decimal Price, bool InStock, int CategoryId);

// What the API accepts to update an existing product (same shape as create —
// the id travels in the URL, not the body).
public record UpdateProductRequest(string Name, decimal Price, bool InStock, int CategoryId);

// What the API returns for a category.
public record CategoryDto(int Id, string Name, int ProductCount);
