import { Component } from "@angular/core";
import { ActivatedRoute, Route, Router } from "@angular/router";
import { ProductService } from "../../../services/product.service";
import { Product } from "../../../models/product";
import { FormsModule } from "@angular/forms";

/**
 * DAY 5, TASK C — The Modification Interface.
 *
 * Route: /dashboard/products/:id/edit
 *
 *   1. Fetch the product by id and pre-populate the form (patchValue).
 *   2. On submit, package the edited values into a payload.
 *   3. Fire an HTTP PUT through ProductService.update() to save it on the API.
 */
@Component({
  selector: "app-product-edit",
  standalone: true,
  imports: [FormsModule],
  templateUrl: "./product-edit.component.html",
})
export class ProductEditComponent {
  constructor(
    private route: ActivatedRoute,
    private route1: Router,
    private productService: ProductService,
  ) {}
  routeParams = this.route.snapshot.paramMap;
  name = "";
  price = 0;
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
  productIdFromRoute = 0;
  ngOnInit() {
    this.productIdFromRoute = Number(this.routeParams.get("id"));
    this.productService.getProductById(this.productIdFromRoute).subscribe({
      next: (product) => (
        (this.name = product.name),
        (this.price = product.price)
      ),
    });
  }
  onSubmit() {
    this.productService
      .update(this.productIdFromRoute, {
        name: this.name,
        price: this.price,
        inStock: true,
        category: "",
        categoryId: 1,
        sku: "",
        description: "",
        title: "",
      })
      .subscribe({
        next: (product) => {
          this.route1.navigate(["./"]);
        },
      });

    // console.log(
    //   this.productService.update(this.productIdFromRoute, {
    //     name: this.name,
    //     price: this.price,
    //     inStock: true,
    //     category: "",
    //     sku: "",
    //     description: "",
    //     title: "",
    //   }),
    // );
  }
}
