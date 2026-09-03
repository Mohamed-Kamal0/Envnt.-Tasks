import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';

/**
 * App overview — tasks for the pieces stubbed in this project (fill them in).
 * (#Task 1 in app.config.ts and #Task 3 in product.service.ts already exist
 * as real, working files — see their own inline comments.)
 *
 * #Task 2 — Nested dashboard routes. Path:
 * `src/app/pages/dashboard/dashboard.routes.ts`. This is a plain routes
 * file, not something `ng generate` scaffolds — fill in the stub.
 * Import DashboardComponent, ProductListComponent, ProductDetailComponent,
 * and ProductEditComponent, then populate DASHBOARD_ROUTES with the routes
 * rendered inside DashboardComponent's own nested router-outlet: a default
 * redirect to the product list, a route for the product list itself
 * (Task A), a route for viewing a single product's details (Task B), and a
 * route for editing a single product (Task C). The edit route's path is
 * more specific than the plain detail route's path (it has an extra
 * trailing segment), so it must be registered before the detail route —
 * otherwise the router will try to match that extra segment as if it were
 * part of the product id and never reach the edit screen. authGuard on the
 * parent /dashboard route in app.routes.ts already protects everything
 * underneath it, so these nested routes don't need to be guarded again.
 * Until this file exists and is filled in, /dashboard renders its shell
 * (sidebar + "Sign out") with a blank area where the outlet content should
 * be — it will not crash.
 *
 * #Task 4 — ProductListComponent (DAY 5, TASK A — The Master Catalog
 * Panel). Path: `src/app/pages/dashboard/product-list/product-list.component.ts`
 * + `.html`. Scaffold with `ng generate component pages/dashboard/product-list
 * --standalone`. Inject ProductService, implement OnInit so the product
 * list loads as soon as the component initializes, and fill in a load()
 * method that tracks a loading state, clears any previous error message,
 * and asks ProductService for the full list of products — populating a
 * `products: Product[]` array on success and setting an error message on
 * failure. Then flesh out the template with a table showing one row per
 * product (its sku, title, and price) plus "View" and "Edit" links that
 * navigate to that product's detail and edit routes using absolute router
 * links built from the product's id, plus loading/error blocks following
 * the same pattern as products.component.html.
 *
 * #Task 5 — ProductDetailComponent logic (DAY 5, TASK B — Targeted Unique
 * Record Query). Path:
 * `src/app/pages/dashboard/product-detail/product-detail.component.ts`.
 * Scaffold with `ng generate component pages/dashboard/product-detail
 * --standalone`. Route: /dashboard/products/:id. The :id segment arrives as
 * a component input (withComponentInputBinding must be enabled in
 * app.config.ts — see #Task 1), which should be fed to ProductService to
 * fetch one product. Inject ProductService, and use a property setter/getter
 * pair for `id` instead of a plain @Input() field — the setter should
 * remember the incoming value and trigger a fetch every time it runs, so
 * navigating between two product ids on the same route re-triggers a fetch
 * (a plain field or ngOnInit alone would miss that). Implement a fetch()
 * method that does nothing if there is no current id, otherwise sets a
 * loading state, clears any error message, and asks ProductService for the
 * product matching the current id (converted to a number), populating a
 * `product?: Product` field on success and an error message on failure.
 *
 * #Task 6 — ProductDetailComponent template. Path:
 * `product-detail.component.html`. Once `product` is populated by fetch(),
 * render its full details — title, sku, description, and price — plus an
 * "Edit this product" link that navigates to that product's edit route
 * using an absolute router link built from its id. Keep the back link and
 * the loading/error message blocks that already exist for this route.
 *
 * #Task 7 — ProductEditComponent and its form (DAY 5, TASK C — The
 * Modification Interface). Path:
 * `src/app/pages/dashboard/product-edit/product-edit.component.ts` +
 * `.html`. Scaffold with `ng generate component pages/dashboard/product-edit
 * --standalone`. Build a reactive form using FormBuilder, with non-nullable
 * typed controls for title, sku, price, and description. Add validation
 * requiring the title to be present and at least 3 characters, the sku to
 * be present, the price to be present and a positive number, and the
 * description to be present and at least 10 characters. Then bind the form
 * to the template with one field per control, a submit button disabled
 * while invalid or saving, and a Cancel link back to the detail page.
 *
 * #Task 8 — ProductEditComponent.ngOnInit(). Same file. When the component
 * initializes, set a loading state and ask ProductService for the product
 * matching this component's id, pre-filling the form's values from the
 * fetched product on success, and clearing the loading state (and setting
 * an error message on failure) either way.
 *
 * #Task 9 — ProductEditComponent.onSubmit(). Same file. Mark every control
 * as touched so validation messages show up, and stop here if the form is
 * invalid. Otherwise, read the form's current values (including any
 * disabled controls, which plain `.value` would skip) to build the update
 * payload, call ProductService.update() with this product's id and that
 * payload, and on success navigate back to this product's detail page.
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
