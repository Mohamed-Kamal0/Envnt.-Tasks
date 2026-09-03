// The shape of one catalog product. It deliberately mirrors the API's ProductDto
// (api/CatalogApi/Dtos/CatalogDtos.cs) — one shape, two languages, no guessing.
export interface Product {
  id: number;
  name: string;
  category: string;
  price: number;
  inStock: boolean;
}

// A typed helper: takes a Product, returns a display string like "$89.99".
export function formatPrice(p: Product): string {
  return `$${p.price.toFixed(2)}`;
}
