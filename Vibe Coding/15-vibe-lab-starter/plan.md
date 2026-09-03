# Plan: Add a min-price filter

## Ticket

### Goal
Add a `minPrice` filter to the product list so users can limit results to products priced at or above a chosen value.

### Context (files involved)
- API controller: `api/CatalogApi/Controllers/ProductsController.cs`
- API service contract: `api/CatalogApi/Services/ICatalogService.cs`
- API service implementation: `api/CatalogApi/Services/CatalogService.cs`
- Angular data service: `web/src/app/product.service.ts`
- Angular product list UI: `web/src/app/product-list.ts`

### Constraints
- Keep existing loading and error states unchanged.
- Do not add NuGet/npm packages.
- Filtering must be server-side (`GET /api/products?minPrice=...`), not client-only.
- Clearing the min-price input must return full results again.
- Keep code aligned with existing project conventions.

### Example (expected result)
- `GET /api/products?minPrice=40` returns only products with `price >= 40` (e.g., Mechanical Keyboard, Webcam 1080p, 27" Monitor, Ultrawide Monitor).
- Entering `50` in the UI hides products below `$50`.
- Clearing the min-price input shows all products again.

### Output format
1. Backend changes summary (controller + service contract + service logic)
2. Frontend changes summary (input + query param wiring)
3. Verification evidence (`curl` + UI behavior checks)
4. Final change list/diff summary

### Done when (observable)
- UI filter works: entering `50` limits results to products priced `>= 50`; clearing restores all.
- API filter works: `curl "http://localhost:5144/api/products?minPrice=40"` matches UI-visible set.
- Existing loading and error behavior still works.
- No dependency changes were introduced.

## Chosen approach and why
**Chosen approach:** server-side filtering in `CatalogService`, exposed through `minPrice` query parameter in the API, and consumed by the Angular client.

**Why this approach:** it keeps the API as the source of truth, ensures consistent behavior across UI and direct API calls, and avoids fetching data that will be filtered out in the browser.

## Implementation steps (in order)
1. Update API contract:
   - Add optional `minPrice` query parameter to `ProductsController.Get`.
   - Pass `minPrice` into `ICatalogService.GetProductsAsync`.
2. Update service interface:
   - Extend `ICatalogService.GetProductsAsync` signature to accept `decimal? minPrice`.
3. Implement service-side filter:
   - In `CatalogService.GetProductsAsync`, apply `p.Price >= minPrice` when `minPrice` has a value.
4. Wire frontend request:
   - Extend `ProductService.load` to accept `minPrice: number | null`.
   - Append `minPrice` query param only when provided and finite.
5. Add frontend input and reload behavior:
   - Add numeric min-price input in `product-list.ts` toolbar.
   - Bind input to component state and call `reload()` on change.
   - Pass `query`, `sort`, and `minPrice` to `ProductService.load`.
6. Verify behavior:
   - Confirm API endpoint with `curl` at multiple values (`40`, `50`, empty, invalid).
   - Confirm UI behavior for set/clear min price and unchanged loading/error states.
