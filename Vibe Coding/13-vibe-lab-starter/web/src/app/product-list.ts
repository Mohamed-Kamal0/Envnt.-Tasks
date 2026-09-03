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
    this.svc.load(this.query, this.sort());
  }
}
