// #Task 1: Give the interface a numeric id, a text title, a numeric price,
// a text SKU code, and a text description field so ProductsComponent's
// mock array type-checks against it.
export interface Product {
  id: number;
  title: string;
  price: number;
  SKU: string;
  description: string;
  category: string;
}
