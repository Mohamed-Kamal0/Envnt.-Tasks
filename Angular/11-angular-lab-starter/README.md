> 🚧 **This is the STUDENT STARTER version** — the dashboard CRUD (product list/detail/edit, nested routes, PUT update) is intentionally incomplete. Follow the `#Task` comments in the listed files. See `ecommerce-startup/day-05` for the completed reference solution if you get stuck.

The nested dashboard routes and all three product-list/detail/edit pages
don't exist yet in this project — you create them from scratch. `ng serve`
will show missing-file/module errors until they exist; that's expected.

## 🎯 Your Tasks

1. **Enable route-param-to-input binding** — turn on Angular's
   route-parameter-to-input binding feature where the router is provided.
   Without it, the `:id` route param won't reach the `@Input()` properties in
   `ProductDetailComponent` / `ProductEditComponent`.
   Path: `src/app/app.config.ts`

2. **Create the nested dashboard routes** — this file does not exist yet;
   create it from scratch (it's a plain routes file, not something `ng
   generate` scaffolds). Define the three nested routes rendered inside
   `DashboardComponent`'s own router outlet — the product list (Task A), the
   single-product detail view (Task B), and the single-product edit screen
   (Task C) — plus a default redirect to the product list. The edit route's
   path is more specific than the plain detail route's path, so it must be
   registered first, or the router will try to match part of it as the
   product id. Until this file exists and is filled in, `/dashboard` renders
   its shell (sidebar + "Sign out") with a blank area where the outlet
   content should be — it will not crash.
   Path: `src/app/pages/dashboard/dashboard.routes.ts`

3. **Implement `ProductService.update()`** — implement `update(id, payload)`
   so it sends the payload to this product's endpoint via HTTP PUT and returns
   the updated product. `getProducts()` and `getProductById()` are already
   done for you and untouched.
   Path: `src/app/services/product.service.ts`

4. **Create `ProductListComponent`** (Task A) — this component does not exist
   yet. Scaffold it with `ng generate component pages/dashboard/product-list
   --standalone`, then inject `ProductService`, fetch the product list on
   init, track loading/error state, and render a table with one row per
   product (SKU / Title / Price) plus View/Edit links built from each
   product's id.
   Path: `src/app/pages/dashboard/product-list/product-list.component.ts` and
   `product-list.component.html`

5. **Create `ProductDetailComponent` logic** (Task B) — this component does
   not exist yet. Scaffold it with `ng generate component
   pages/dashboard/product-detail --standalone`, then implement the `id`
   property using a setter/getter pair (not a plain `@Input() id`), so that
   navigating between two product ids on the same route re-triggers a fetch.
   The setter should trigger a `fetch()` method that loads the product
   matching the current id.
   Path: `src/app/pages/dashboard/product-detail/product-detail.component.ts`

6. **Build the `ProductDetailComponent` template** (Task B) — once the
   product is loaded, render its title, sku, description, and price, plus an
   "Edit this product" link to that product's edit route.
   Path: `src/app/pages/dashboard/product-detail/product-detail.component.html`

7. **Create `ProductEditComponent` and its form** (Task C) — this component
   does not exist yet. Scaffold it with `ng generate component
   pages/dashboard/product-edit --standalone`, then build the reactive form
   (using `FormBuilder`) with validation requiring the title to be present
   and at least 3 characters, the sku to be present, the price to be present
   and a positive number, and the description to be present and at least 10
   characters — then bind that form to the template with one field per
   control, a submit button disabled while invalid or saving, and a Cancel
   link back to the detail page.
   Path: `src/app/pages/dashboard/product-edit/product-edit.component.ts` and
   `product-edit.component.html`

8. **Implement `ProductEditComponent.ngOnInit()`** (Task C) — implement
   Angular's `ngOnInit` lifecycle hook to fetch the product and pre-fill the
   form with its values.
   Path: `src/app/pages/dashboard/product-edit/product-edit.component.ts`

9. **Implement `ProductEditComponent.onSubmit()`** — implement `onSubmit()`
   to validate the form, build the update payload from its current values,
   call `ProductService.update()`, and navigate back to the detail page on
   success.
   Path: `src/app/pages/dashboard/product-edit/product-edit.component.ts`

---

# Day 05 — Admin Dashboard (Product CRUD)

## Hands-on

- **Task A — The Master Catalog Panel**: a clean, data-dense table in the
  dashboard listing every product row currently in the database.
- **Task B — Targeted Unique Record Query**: a nested route
  `/dashboard/products/:id` that reads the id via Angular's router and shows a
  single detailed product view.
- **Task C — The Modification Interface**: an `edit-product` screen,
  pre-populated with the fetched product, that packages the edited values into
  a payload and fires an HTTP `PUT` to update the product on the .NET API.

## Run it

```bash
npm install
npm start
```

Sign in at `/login` (any email + 6-character password while `useMockAuth` is
`true`), then open **Dashboard** in the navbar.

## Routes

| Path | Guard | Component |
| --- | --- | --- |
| `/dashboard` | `authGuard` | `DashboardComponent` (layout: sidebar + outlet) |
| `/dashboard/products` | inherited | `ProductListComponent` — Task A |
| `/dashboard/products/:id` | inherited | `ProductDetailComponent` — Task B |
| `/dashboard/products/:id/edit` | inherited | `ProductEditComponent` — Task C |

The whole `/dashboard/**` subtree is one `loadChildren` chunk (see
`app.routes.ts`), and `authGuard` sits on the parent route only — every child
route inherits that protection for free.

`.../:id/edit` is declared **before** `.../:id` in `dashboard.routes.ts`;
otherwise the router would try to parse `"edit"` as a product id.

## Expected API endpoints

```
GET  /api/products         ->  Product[]
GET  /api/products/{id}    ->  Product
PUT  /api/products/{id}    ->  Product      (Task C)
```

```jsonc
// PUT body — no id, it's in the URL
{ "title": "Wireless Headphones", "price": 1999, "sku": "AUD-001", "description": "…" }
```

## How Task C actually works

1. `ProductEditComponent.ngOnInit()` calls `getProductById(id)` and
   `this.form.patchValue(product)` — the reactive form's inputs fill with the
   existing values.
2. The user edits a field.
3. `onSubmit()` reads `this.form.getRawValue()` (a `ProductPayload` — the
   `Product` shape minus `id`) and calls `productService.update(id, payload)`.
4. `ProductService.update()` does `this.http.put<Product>(url, payload)`.
5. On success, the user is routed back to the detail page for that product.

## Try it yourself

1. Add a delete button to `ProductListComponent` that calls a new
   `ProductService.delete(id)` (`HttpClient.delete`).
2. Add a "create product" screen re-using `ProductEditComponent`'s form (the
   pattern used by `cartly-store`'s `product-form` component: one form, two
   modes, decided by whether `:id` is present in the route).
3. Add a loading skeleton to `ProductDetailComponent` instead of the plain
   "Loading…" text.
