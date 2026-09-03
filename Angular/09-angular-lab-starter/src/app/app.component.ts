import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';

/**
 * App overview — tasks for the pieces that don't exist yet in this project.
 *
 * #Task 1 — Environment config. Path: `src/app/environments/environment.ts`.
 * Create this file exporting an `environment` object with a
 * `production: false` flag and an `apiUrl` string pointing at your .NET
 * API's base URL (e.g. an absolute URL like `https://localhost:7297/api`,
 * or a relative `/api` if you plan to use the dev-server proxy).
 *
 * #Task 2 — Production environment config. Path:
 * `src/app/environments/environment.prod.ts`. Same shape, with
 * `production: true` and an `apiUrl` pointing at a real deployed API origin.
 *
 * #Task 3 — Dev-server proxy config. Path: `proxy.conf.json` (project root).
 * Map the `/api` path prefix to your .NET API's local origin (e.g.
 * `https://localhost:7297`), so relative API calls during `ng serve` get
 * forwarded there instead of hitting the Angular dev server itself.
 *
 * #Task 5 — ProductService setup. Path:
 * `src/app/services/product.service.ts`. Create it (e.g. via
 * `ng g s services/product`), inject HttpClient, and add a class property
 * holding the base URL for the products endpoint, built from the
 * environment configuration's API URL.
 *
 * #Task 6 — ProductService.getProducts(). Same file. Send a GET request to
 * the products endpoint and return the resulting observable of products.
 * Note: if your real API wraps the response in an envelope object (e.g. a
 * `{ data: [...] }` shape) instead of returning a bare array, you'll need to
 * unwrap it — check your Network tab for the actual response shape.
 *
 * #Task 7 — ProductService.getProductById(id). Same file. Send a GET request
 * for a single product by id and return the resulting observable of that
 * product, with the same envelope caveat as above.
 *
 * #Task 8 — ProductCardComponent. Path:
 * `src/app/components/product-card/product-card.component.ts` + `.html`.
 * Create a standalone component with a required `Product` input named
 * `productData`, and build its template to display the product's fields —
 * something like a SKU label, a title heading, a short description, and a
 * formatted price.
 *
 * #Task 4 (app.config.ts) and #Task 9-11 (products.component.ts/.html)
 * already exist as real, working files — see their own inline comments.
 */
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, FooterComponent],
  template: `
    <app-navbar />
    <router-outlet />
    <app-footer />
  `,
})
export class AppComponent {}
