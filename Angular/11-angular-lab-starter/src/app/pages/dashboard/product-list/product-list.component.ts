import { Component, inject } from "@angular/core";
import { ProductService } from "../../../services/product.service";
import { Product } from "../../../models/product";
import { ProductCardComponent } from "../../../components/product-card/product-card.component";
import { Router } from "@angular/router";
import { RouterLink } from "@angular/router";
/**
 * DAY 5, TASK A — The Master Catalog Panel.
 * A clean, data-dense table listing every product row in the database.
 */
@Component({
  selector: "app-product-list",
  standalone: true,
  imports: [ProductCardComponent, RouterLink],
  templateUrl: "./product-list.component.html",
})
export class ProductListComponent {
  private readonly productService = inject(ProductService);
  private router = inject(Router);
  products: Product[] = [];
  loading = false;
  errorMessage = "";

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.errorMessage = "";

    this.productService.getProducts().subscribe({
      next: (products) => {
        this.products = products;
        this.loading = false;
      },
      error: () => {
        this.errorMessage =
          "Could not reach the API. Is the .NET backend running?";
        this.loading = false;
      },
    });
  }
  editProduct(id: number) {
    console.log(this.router.url + `/${id}`);
    this.router.navigate([this.router.url + `/${id}/edit`]);
  }
  viewProduct(id: number) {
    console.log(this.router.url + `/${id}`);
    this.router.navigate([this.router.url + `/${id}`]);
  }
}
