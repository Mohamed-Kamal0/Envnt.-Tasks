import { Component, Input } from "@angular/core";
import { Product, formatPrice } from "./product";

// Presentational: it takes an @Input() and renders. No service, no state, no
// fetching — the parent decides everything. Easy to review, easy to reuse.
@Component({
  selector: "app-product-card",
  standalone: true,
  template: `
    <article class="card" [class.card--out]="!product.inStock">
      <div class="card__media">
        <span class="category-pill">{{ product.category }}</span>
      </div>

      <div class="card__body">
        <h3 class="card__name">{{ product.name }}</h3>

        <div class="card__meta">
          <p class="card__price">{{ price }}</p>
          <p class="card__stock">
            @if (product.inStock) {
              <i class="fa-solid fa-check"></i> In stock
            } @else {
              <i class="fa-solid fa-xmark"></i> Sold out
            }
          </p>
        </div>
      </div>
    </article>
  `,
})
export class ProductCard {
  @Input({ required: true }) product!: Product;

  get price(): string {
    return formatPrice(this.product);
  }
}
