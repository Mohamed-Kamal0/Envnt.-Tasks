export interface Product {
  id: number;
  name: string;
  price: number;
  category?: string;
  categoryId?: number;
  title?: string;
  sku?: string;
  inStock?: boolean;
  description?: string;
}

/** Body sent on PUT /products/{id} — no id inside the payload, it's in the URL. */
// Pre-defined for you — this is the payload shape used by
// ProductService.update() (#Task 3) and by the edit form's reactive form
// (#Task 7).
export type ProductPayload = Omit<Product, "id">;
