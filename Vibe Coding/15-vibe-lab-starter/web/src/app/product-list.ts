import { Component, OnInit, inject, signal } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { ProductCard } from "./product-card";
import { ProductService } from "./product.service";

// The three sort states the API understands. "" means "leave the order alone",
// which is also what the API does with anything it doesn't recognise.
type SortOption = "" | "price_asc" | "price_desc";

// The catalog page: a search box, a sort toggle, the request state machine, and a
// grid of cards. Filtering AND sorting happen in the API — this component decides
// what to ask for, never how to compute it.
@Component({
  selector: "app-product-list",
  standalone: true,
  imports: [ProductCard, FormsModule],
  template: `
    <section class="catalog-panel">
      <div class="toolbar">
        <div class="toolbar__heading">
          <span class="eyebrow">Inventory</span>
          <h2>Products</h2>
        </div>

        <div class="toolbar__controls">
          <label class="search-wrap">
            <i class="fa-solid fa-magnifying-glass"></i>
            <input
              class="form-control search"
              type="search"
              placeholder="Search products…"
              [(ngModel)]="query"
              (ngModelChange)="reload()"
            />
          </label>

          <input
            class="form-control"
            type="number"
            placeholder="Min price"
            min="0"
            step="0.01"
            [(ngModel)]="minPrice"
            (ngModelChange)="reload()"
          />

          <input
            class="form-control"
            type="number"
            placeholder="Max price"
            min="0"
            step="0.01"
            [(ngModel)]="maxPrice"
            (ngModelChange)="reload()"
          />

          <button class="sort-button" (click)="cycleSort()">
            <i class="fa-solid fa-arrow-down-wide-short"></i>
            Price: {{ sortLabel }}
          </button>
        </div>
      </div>

      @if (svc.loading()) {
        <p class="catalog-state catalog-state--loading">
          <i class="fa-solid fa-spinner fa-spin"></i> Loading products…
        </p>
      } @else if (svc.error()) {
        <p class="catalog-state catalog-state--error">
          <i class="fa-solid fa-triangle-exclamation"></i> {{ svc.error() }}
        </p>
      } @else if (svc.products().length === 0) {
        <p class="catalog-state catalog-state--empty">No products match "{{ query }}".</p>
      } @else {
        <div class="grid">
          @for (p of svc.products(); track p.id) {
            <app-product-card [product]="p" />
          }
        </div>
      }
    </section>
  `,
})
export class ProductList implements OnInit {
  svc = inject(ProductService);

  query = "";
  minPrice: number | null = null;
  maxPrice: number | null = null;
  sort = signal<SortOption>("");

  ngOnInit(): void {
    this.reload();
  }

  get sortLabel(): string {
    switch (this.sort()) {
      case "price_asc":
        return "Low → High";
      case "price_desc":
        return "High → Low";
      default:
        return "Default";
    }
  }

  // Default → Low→High → High→Low → Default. Three states, one button.
  cycleSort(): void {
    this.sort.update((current) =>
      current === "" ? "price_asc" : current === "price_asc" ? "price_desc" : "",
    );
    this.reload();
  }

  // Every toolbar change re-asks the API. One place decides what the server sees.
  reload(): void {
    this.svc.load(this.query, this.sort(), this.maxPrice, this.minPrice);
  }
}
