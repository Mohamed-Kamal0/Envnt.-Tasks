> 🚧 **This is the STUDENT STARTER version** — the real API connection (ProductService, ProductCardComponent, HttpClient wiring) is intentionally incomplete. Follow the `#Task` comments in the listed files. See `ecommerce-startup/day-03` for the completed reference solution if you get stuck.

The environment config, proxy config, ProductService, and ProductCardComponent don't exist yet in this project — you create them from scratch. `ng serve` will show missing-file/module errors until they exist; that's expected.

## 🎯 Your Tasks

- [ ] **#Task 1** (`src/app/environments/environment.ts`) — Create this file exporting an `environment` object with a `production: false` flag and an `apiUrl` string pointing at your .NET API's base URL (e.g. an absolute URL like `https://localhost:7297/api`, or a relative `/api` if you plan to use the dev-server proxy).
- [ ] **#Task 2** (`src/app/environments/environment.prod.ts`) — Create the production variant of the environment file, exporting an `environment` object with `production: true` and an `apiUrl` pointing at a real deployed API origin.
- [ ] **#Task 3** (`proxy.conf.json`) — Create a dev-server proxy config file (project root) mapping the `/api` path prefix to your .NET API's local origin (e.g. `https://localhost:7297`), so relative API calls during `ng serve` get forwarded there instead of hitting the Angular dev server itself.
- [ ] **#Task 4** (`src/app/app.config.ts`) — Wire Angular's HttpClient up as an application-wide provider so it can be injected anywhere.
- [ ] **#Task 5** (`src/app/services/product.service.ts`) — Create `ProductService` (e.g. via `ng g s services/product`), inject HttpClient into it, and add a class property holding the base URL for the products endpoint, built from the environment configuration's API URL.
- [ ] **#Task 6** (`src/app/services/product.service.ts`) — Implement `getProducts()` to fetch the product list from the API.
- [ ] **#Task 7** (`src/app/services/product.service.ts`) — Implement `getProductById(id)` to fetch a single product from the API.
- [ ] **#Task 8** (`src/app/components/product-card/product-card.component.ts`, `src/app/components/product-card/product-card.component.html`) — Create `ProductCardComponent` as a standalone component with a required `Product` input property, and build its template to display the product's fields — something like a SKU label, a title heading, a short description, and a formatted price.
- [ ] **#Task 9** (`src/app/components/products/products.component.ts`) — Inject `ProductService` into `ProductsComponent` and add loading/error state properties.
- [ ] **#Task 10** (`src/app/components/products/products.component.ts`) — Implement `OnInit` and a method that loads products and updates loading/error/product state, calling it on init.
- [ ] **#Task 11** (`src/app/components/products/products.component.html`) — Build the loading, error, and success template branches around the product grid.

# Day 03 — Real .NET API + Child Component

## Hands-on

1. **HTTP Configuration** — add `provideHttpClient()` in `app.config.ts`.
2. **Data Service Construction** — `ng g s services/product` → `ProductService`.
3. **API Connectivity Integration** — inject `HttpClient`, write `getProducts()` calling the live .NET Core API.
4. **Bonus — Parent to Child Input Pipeline** — a nested `ProductCardComponent` takes over rendering one product.
5. **Property Binding Implementation** — `products.component.html` passes each item down via `[productData]="product"` instead of rendering markup itself.

## Run it

You need your .NET API running first.

```bash
npm install
npm start
```

`npm start` runs `ng serve --proxy-config proxy.conf.json`, which forwards every
`/api/*` request to `https://localhost:5001` (edit `proxy.conf.json` to match your
API's actual port). Because the browser only ever talks to `localhost:4200`, no
CORS setup is needed while developing.

If you'd rather call the API directly, set `apiUrl` in
`src/app/environments/environment.ts` to the full URL and run
`npm run start:direct` — then add a CORS policy on the .NET side (see the
`cartly-store` project's README for the exact `Program.cs` snippet).

## Expected API endpoint

```
GET /api/products  ->  Product[]
```

```jsonc
{ "id": 1, "title": "Wireless Headphones", "price": 1899, "sku": "AUD-001", "description": "…" }
```

If your API's controller returns different field names (e.g. `Name` instead of
`title`, PascalCase instead of camelCase), edit `src/app/models/product.ts` and
`product-card.component.html` to match.

## What changed since Day 2

- `ProductsComponent` no longer holds a local array — it injects `ProductService`
  and calls `getProducts()` in `ngOnInit()`, with loading and error states.
- A brand-new `ProductCardComponent` renders a single product. `ProductsComponent`
  loops with `@for` and, instead of writing the card markup itself, does:

  ```html
  <app-product-card [productData]="product" />
  ```

  `ProductCardComponent` receives it as `@Input({ required: true }) productData!: Product;`
  — this is the parent-to-child data flow every Angular app relies on.

## Try it yourself

1. Add a `getProductById(id)` call and try it from the browser console.
2. Make `ProductCardComponent` emit an `@Output() selected = new EventEmitter<Product>()`
   when clicked, and log it from `ProductsComponent`.
3. Handle a `404` from the API differently from a `500` in the error message.
