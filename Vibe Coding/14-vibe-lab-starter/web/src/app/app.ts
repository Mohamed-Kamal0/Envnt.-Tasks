import { Component, OnInit, inject } from "@angular/core";
import { ProductList } from "./product-list";
import { ProductService } from "./product.service";

// The root shell: a featured-product banner over the catalog page.
@Component({
  selector: "app-root",
  standalone: true,
  imports: [ProductList],
  template: `
    <div class="app">
      <header class="topbar">
        <h1><i class="fa-solid fa-store"></i> Product Catalog</h1>
        <span class="text-muted">.NET API + Angular · Vibe Coding bench</span>
      </header>

      @if (svc.featured(); as f) {
        <div class="banner">
          Featured today: <strong>{{ f.name }}</strong> — \${{ f.price.toFixed(2) }}
        </div>
      } @else if (svc.featuredError()) {
        <div class="banner text-danger">
          <i class="fa-solid fa-triangle-exclamation"></i> {{ svc.featuredError() }}
        </div>
      }

      <main>
        <app-product-list />
      </main>
    </div>
  `,
})
export class App implements OnInit {
  svc = inject(ProductService);

  ngOnInit(): void {
    this.svc.loadFeatured();
  }
}
