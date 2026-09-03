import { Component, Input } from "@angular/core";
import { Product, formatPrice } from "./product";

// Presentational: it takes an @Input() and renders. No service, no state, no
// fetching — the parent decides everything. Easy to review, easy to reuse.
@Component({
  selector: "app-product-card",
  standalone: true,
  template: `
    <article class="card p-3" [class.card--out]="!product.inStock">
      <h3 class="card__name">{{ product.name }}</h3>
      <span class="badge text-bg-secondary">{{ product.category }}</span>

      <p class="card__price">{{ price }}</p>

      <p class="card__stock">
        @if (product.inStock) {
          <i class="fa-solid fa-check text-success"></i> In stock
        } @else {
          <i class="fa-solid fa-xmark text-danger"></i> Sold out
        }
      </p>
    </article>
  `,
})
export class ProductCard {
  @Input({ required: true }) product!: Product;

  get price(): string {
    return formatPrice(this.product);
  }
}
