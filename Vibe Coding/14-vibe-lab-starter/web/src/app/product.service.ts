import { Injectable, inject, signal } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Product } from "./product";
import { environment } from "../environments/environment";

// An app-wide singleton holding the request state machine you built in Week 2:
// loading / error / products, plus the featured product for the banner.
@Injectable({ providedIn: "root" })
export class ProductService {
  private http = inject(HttpClient);
  private base = `${environment.apiBase}/products`;

  readonly products = signal<Product[]>([]);
  readonly loading = signal(false);
  readonly error = signal<string | null>(null);

  readonly featured = signal<Product | null>(null);
  readonly featuredError = signal<string | null>(null);

  // GET /api/products?search=…&sort=…&cheapOnly=…
  load(search = "", sort = "", cheapOnly = false): void {
    this.loading.set(true);
    this.error.set(null);

    let params = new HttpParams();
    if (search) params = params.set("search", search);
    if (sort) params = params.set("sort", sort);
    if (cheapOnly) params = params.set("cheapOnly", "true");

    this.http.get<Product[]>(this.base, { params }).subscribe({
      next: (data) => {
        this.products.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set("Could not load products. Is the .NET API running on port 5144?");
        this.loading.set(false);
      },
    });
  }

  // GET /api/products/featured — fills the banner.
  loadFeatured(): void {
    this.featuredError.set(null);
    this.http.get<Product>(`${this.base}/featured`).subscribe({
      next: (p) => this.featured.set(p),
      error: (err) =>
        this.featuredError.set(
          `Featured product unavailable (HTTP ${err.status}). Read the API console.`,
        ),
    });
  }

  // POST /api/products/sync — pushes the catalog upstream.
  // The real API key is held server-side; the browser sends no secret.
  sync(): void {
    this.http
      .post(`${this.base}/sync`, {})
      .subscribe({
        next: () => console.log("catalog sync accepted"),
        error: () => console.warn("catalog sync failed"),
      });
  }

}
