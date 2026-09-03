import { Component, OnInit, inject, signal } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { ProductCard } from "./product-card";
import { ProductService } from "./product.service";

type SortOption = "" | "price_asc" | "price_desc";

// The catalog page: search, sort, a budget filter, and a sync button.
// Something here does not do what its label says. Run it before you read it.
@Component({
  selector: "app-product-list",
  standalone: true,
  imports: [ProductCard, FormsModule],
  template: `
    <section>
      <div class="toolbar">
        <h2>Products</h2>

        <input
          class="form-control search"
          type="search"
          placeholder="Search products…"
          [(ngModel)]="query"
          (ngModelChange)="reload()"
        />

        <button class="btn btn-outline-primary" (click)="cycleSort()">
          Price: {{ sortLabel }}
        </button>

        <button class="btn btn-outline-secondary" (click)="toggleCheapOnly()">
          {{ cheapOnly() ? "Show all prices" : "Under $50 only" }}
        </button>

        <button class="btn btn-outline-dark" (click)="svc.sync()">
          <i class="fa-solid fa-rotate"></i> Sync
        </button>
      </div>

      @if (svc.loading()) {
        <p class="status"><i class="fa-solid fa-spinner fa-spin"></i> Loading products…</p>
      } @else if (svc.error()) {
        <p class="text-danger"><i class="fa-solid fa-triangle-exclamation"></i> {{ svc.error() }}</p>
      } @else if (svc.products().length === 0) {
        <p class="status">No products match "{{ query }}".</p>
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
  sort = signal<SortOption>("");
  cheapOnly = signal(false);

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

  cycleSort(): void {
    this.sort.update((current) =>
      current === "" ? "price_asc" : current === "price_asc" ? "price_desc" : "",
    );
    this.reload();
  }

  toggleCheapOnly(): void {
    this.cheapOnly.update((v) => !v);
    this.reload();
  }

  reload(): void {
    this.svc.load(this.query, this.sort(), this.cheapOnly());
  }
}
