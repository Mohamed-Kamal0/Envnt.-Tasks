import { Component } from "@angular/core";

import { Product } from "../../models/product";
import { FormsModule } from "@angular/forms";
import { NgClass } from "@angular/common";
/**
 * DAY 2, STEP 4 (Mock Array Creation) + STEP 5 (Conditional Card Styling).
 *
 * The array below is fully local — no service, no HTTP, just data sitting in
 * the component class. Day 3 replaces it with a real call to the .NET API and
 * moves this array into a ProductService.
 */
@Component({
  selector: "app-products",
  standalone: true,
  imports: [FormsModule, NgClass],
  templateUrl: "./products.component.html",
  styleUrl: "./products.component.css",
})
export class ProductsComponent {
  constructor() {
    console.log("instansiated");
  }

  products: Product[] = [
    {
      id: 1,
      title: "product1",
      price: 10,
      SKU: "product1",
      description: "product1 is good",
      category: "a",
    },
    {
      id: 2,
      title: "product2",
      price: 20,
      SKU: "product2",
      description: "product2 is good",
      category: "b",
    },
    {
      id: 3,
      title: "product3",
      price: 30,
      SKU: "product3",
      description: "product3 is good",
      category: "c",
    },
    {
      id: 4,
      title: "product4",
      price: 40,
      SKU: "product4",
      description: "product4 is good",
      category: "d",
    },
    {
      id: 5,
      title: "product5",
      price: 50,
      SKU: "product5",
      description: "product5 is good",
      category: "e",
    },
    {
      id: 6,
      title: "product6",
      price: 60,
      SKU: "product6",
      description: "product6 is good",
      category: "f",
    },
  ];
  // #Task 6: Populate this array with at least 6 mock Product objects, each
  // supplying its own id, title, price, sku, and description (see
  // models/product.ts for the fields required).
}
