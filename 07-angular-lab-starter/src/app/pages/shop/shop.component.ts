import { Component } from "@angular/core";
import { ProductsComponent } from "../../components/products/products.component";

/**
 * #Task 6: Build a page section with a heading reading "Shop", and beneath
 * it render the already-built Products component (import ProductsComponent
 * into this component's `imports` array, then use its selector in the
 * template).
 */
@Component({
  selector: "app-shop",
  standalone: true,
  imports: [ProductsComponent],
  templateUrl: "./shop.component.html",
  styleUrl: "./shop.component.css",
})
export class ShopComponent {}
