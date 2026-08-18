import { Component } from "@angular/core";

import { Product } from "../../models/product";
import { productService } from "../../services/product.service";
import { ProductCardComponent } from "../product-card/product-card.component";

/**
 * DAY 3, STEP 5 (Property Binding Implementation).
 *
 * The mock array from Day 2 should NOT be re-added here. This component
 * needs to inject ProductService, call getProducts() in ngOnInit, and hand
 * each item to <app-product-card [productData]="product" /> instead of
 * rendering the markup itself.
 *
 * #Task 7: Inject Angular's ProductService into this component, and add
 * class properties to track a loading flag and an error message string.
 *
 * #Task 8: Implement Angular's OnInit lifecycle interface, and write a
 * method that requests the product list from ProductService and updates
 * the loading flag, error message, and products list based on whether the
 * request succeeds or fails. Call that method when the component
 * initializes.
 */
@Component({
  selector: "app-products",
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: "./products.component.html",
  styleUrl: "./products.component.css",
})
export class ProductsComponent {
  products: Product[] = [];
  constructor(private productService: productService) {}
  ngOnInit() {
    this.productService.getProducts().subscribe((products) => {
      this.products = products;
      console.log(products);
    });
    this.productService.getProductById(3).subscribe((product) => {
      console.log(product);
    });
  }
  handleAdd(product: Product) {
    console.log(product);
  }
}
