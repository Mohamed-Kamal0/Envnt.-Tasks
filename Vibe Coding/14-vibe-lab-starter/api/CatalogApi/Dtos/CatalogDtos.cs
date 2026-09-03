namespace CatalogApi.Dtos;

// The boundary rule from Week 1 day 3: entities never cross the door.
// The Angular app is typed against exactly this shape (see web/src/app/product.ts).
public record ProductDto(int Id, string Name, string Category, decimal Price, bool InStock);
