# Student tasks — Week 2 · Day 9: Your Own API + a Child Component

**Today's goal:** replace the mock array with real data from your own .NET API — an environment
config, a proxy, a `ProductService`, and a child `ProductCardComponent` that renders one product
handed down from its parent. **You'll need:** your week-1 API running with
`GET /api/products`, and this day's `starter`.

**Manual-first:** weeks 1–2 are hands-by-you. AI may explain a concept or an error message — it
does not write your service. Any line you can't explain, you redo (the
[JUDGING.md](../../../JUDGING.md) rule).

**Reference solution:** after you've done the manual-first work, check yours against `solution`
(`starter` is where you begin).

## Before you start
- [ ] Your .NET API runs and `GET /api/products` returns a list — check it in the browser or
      Swagger before you touch Angular.
- [ ] Note the port your API is on. You need it twice today.
- [ ] `cd starter && npm install` — `npm start` will fail until you fill in the empty stub files (task 1 onward); that is
      the correct starting state.

## Tasks

### 1 · Point the app at your API  ⏱ ~25
`src/app/environments/environment.ts` is empty — fill it with a `production: false` flag and an
`apiUrl`. `environment.prod.ts` and `proxy.conf.json` are already written for you; open
`proxy.conf.json` and set its `target` to your running API's port (it ships pointing at
`https://localhost:7297`).
**Done when:** `environment.ts` has your API URL, the proxy `target` matches your API, and
`npm start` boots without complaining about them.
Stuck? Relative `apiUrl: '/api'` + the proxy is the path of least pain — absolute URLs need CORS
on the .NET side.

### 2 · Provide HttpClient  ⏱ ~10
In `app.config.ts`, add Angular's HttpClient to the application providers.
**Done when:** injecting `HttpClient` anywhere no longer throws a "no provider" error.

### 3 · ProductService: the list  ⏱ ~25
Fill in `services/product.service.ts` (it ships empty), inject `HttpClient`, build the
products URL from the environment config, and implement `getProducts()`.
**Done when:** calling it returns your API's products — prove it by logging the result once, then
delete the log.

### 4 · ProductService: one product  ⏱ ~10
Add `getProductById(id)` against `GET /api/products/{id}`.
**Done when:** it compiles and returns a single product. Day 11 leans on this.

### 5 · The child component  ⏱ ~30
Fill in `components/product-card/` (the files are there but empty) as a standalone component with a **required** `Product` input,
and build its template: SKU label, title, description, formatted price.
**Done when:** the parent passes one product down with property binding, and TypeScript refuses to
compile if the input is left off.
Stuck? Required inputs are `@Input({ required: true })` — that is the compiler doing your
debugging for you.

### 6 · Loading, error, and the list  ⏱ ~30
In `products.component.ts` inject the service, add loading and error state, and load on init. In
the template, build the three branches: loading, error, and the grid of `<app-product-card>`.
**Done when:** a fresh load shows a loading state briefly, then cards — and the mock array is
gone.

### 7 · The honest test  ⏱ ~10
Stop your API (`Ctrl+C`) and reload the page.
**Done when:** the screen shows your error message, not an eternal spinner and not a blank page.
This is the day's real deliverable.

## Verify

```bash
# API running
cd starter
npm start
```

Then: cards come from the API · DevTools' network tab shows one `/api/products` call · stopping
the API shows the error state.

## End-of-day deliverables
- [ ] `environment.ts` filled in (`environment.prod.ts` and `proxy.conf.json` were provided)
- [ ] `ProductService` with `getProducts()` and `getProductById()`, URL built from the environment
- [ ] `ProductCardComponent` with a required input, rendered by the parent through property
      binding
- [ ] Loading, error and success states all reachable on screen
- [ ] The API stopped mid-session shows an honest error
- [ ] Every line explained ([JUDGING.md](../../../JUDGING.md))

## Finished early?
- Call `getProductById()` from the console and log one product.
- Give `ProductCardComponent` an output that emits the clicked product, and log it from the
  parent.
- Handle a 404 differently from a 500 in the error message, and say why that matters to a user.
