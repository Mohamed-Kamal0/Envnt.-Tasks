namespace CatalogApi.Dtos;

// DTOs — Data Transfer Objects. The boundary rule: entities never cross the door.
// A DTO decides exactly what goes in and out — no over-posting, no accidental fields.

// What the API returns for a product.
public record ProductDto(int Id, string Name, decimal Price, bool InStock);

// What the API accepts to create a product (no Id — the server assigns it).
public record CreateProductRequest(string Name, decimal Price, bool InStock);
