import { Component } from "@angular/core";
import { Product } from "../../../models/product";
import { ActivatedRoute } from "@angular/router";
import { ProductService } from "../../../services/product.service";

/**
 * DAY 5, TASK B — Targeted Unique Record Query.
 *
 * Route: /dashboard/products/:id
 * The `:id` segment arrives as a plain @Input() (withComponentInputBinding()
 * is enabled in app.config.ts), which is fed to ProductService to fetch one
 * product.
 */
@Component({
  selector: "app-product-detail",
  standalone: true,
  imports: [],
  templateUrl: "./product-detail.component.html",
})
export class ProductDetailComponent {
  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
  ) {}
  routeParams = this.route.snapshot.paramMap;

  public product: Product = {
    id: 0,
    name: "",
    price: 0,
    inStock: true,
    category: "",
    sku: "",
    description: "",
    title: "",
  };
  ngOnInit() {
    const productIdFromRoute = Number(this.routeParams.get("id"));
    this.productService.getProductById(productIdFromRoute).subscribe({
      next: (product) => (this.product = product),
    });
  }
}
