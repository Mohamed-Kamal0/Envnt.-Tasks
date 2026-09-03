# Student tasks — Week 2 · Day 11: Admin Dashboard — Product CRUD

**Today's goal:** turn the empty dashboard into a working admin area — list every product, open
one by id, edit it, and PUT the change back to your own API — all behind yesterday's guard.
**You'll need:** your .NET API with `GET /api/products`, `GET /api/products/{id}` and
`PUT /api/products/{id}`, and this day's `starter`.

**Manual-first:** weeks 1–2 are hands-by-you. AI may explain a concept or an error message — it
does not write your components. Any line you can't explain, you redo (the
[JUDGING.md](../../../JUDGING.md) rule).

**Reference solution:** after you've done the manual-first work, check yours against `solution`
(`starter` is where you begin).

## Before you start
- [ ] Your API answers all three product routes, including **PUT** — check before you touch
      Angular.
- [ ] `cd starter && npm install && npm start`, sign in, open Dashboard: you get the sidebar and
      "Sign out" with an empty area beside them. That is the outlet with nothing routed into it
      yet — not a crash.
- [ ] You've read the nine `#Task` comments in the starter.

## Tasks

### 1 · Let route params reach inputs  ⏱ ~10
Where the router is provided, turn on Angular's route-parameter-to-input binding.
**Done when:** it is on. Without it, `:id` never reaches the components you write in tasks 5 and
7, and they fail in a way that looks like your code's fault.

### 2 · The nested routes  ⏱ ~25
Fill in `pages/dashboard/dashboard.routes.ts` (stubbed for you). Three children rendered in the dashboard's
own outlet — list, detail, edit — plus a default redirect to the list.
**Done when:** `/dashboard/products` loads the list route. Register the **edit** route before the
plain detail route.
Stuck? `:id/edit` and `:id` both match `products/7/edit` — first one registered wins, so order is
the whole trick.

### 3 · `ProductService.update()`  ⏱ ~15
Implement `update(id, payload)`: PUT the payload to that product's endpoint, return the updated
product.
**Done when:** it compiles and the URL it builds matches what your API actually exposes.

### 4 · The product table (Task A)  ⏱ ~35
Fill in `pages/dashboard/product-list/` (stubbed for you), inject `ProductService`, fetch on init, track loading
and error, and render a table: one row per product with SKU, title, price, and View / Edit links
built from the product's id.
**Done when:** every product in your database has a row, and the links point at real URLs you can
paste into the address bar.

### 5 · The detail view (Task B)  ⏱ ~30
Fill in `pages/dashboard/product-detail/` (stubbed for you). Implement `id` as a **setter/getter pair**, not a
plain input, and have the setter trigger the fetch. Render title, SKU, description and price, plus
an "Edit this product" link.
**Done when:** navigating straight from product 3's detail page to product 4's re-fetches — a
plain `@Input() id` would leave the old product on screen, which is exactly the bug this avoids.

### 6 · The edit form (Task C)  ⏱ ~35
Fill in `pages/dashboard/product-edit/` (stubbed for you) and build a **reactive** form with `FormBuilder`: title
required and 3+ characters, sku required, price required and positive, description required and
10+ characters. One field per control, a submit button disabled while invalid or saving, and a
Cancel link back to the detail page. Then implement `ngOnInit()` to fetch the product and pre-fill
the form.
**Done when:** opening the edit screen shows the product's current values, and a two-character
title disables the button.

### 7 · Save it for real  ⏱ ~20
Implement `onSubmit()`: check validity, build the payload from the form's values, call
`ProductService.update()`, and go back to the detail page on success.
**Done when:** an edit survives a full page refresh — because it is in your database, not just in
the browser.

## Verify

```bash
# API running
cd starter
npm start
```

Then: sign in → Dashboard → the table lists your products → View opens one → Edit changes it →
the detail page and the database both show the new value.

## End-of-day deliverables
- [ ] Route-param-to-input binding on; `dashboard.routes.ts` with three children and a default
      redirect, edit registered before detail
- [ ] `ProductService.update()` doing a real PUT
- [ ] Product table with loading and error states and working View / Edit links
- [ ] Detail view driven by a setter, so switching ids re-fetches
- [ ] Reactive edit form with all four validation rules, pre-filled, saving to the API
- [ ] `/dashboard/**` still unreachable signed out — the guard on the parent covers the children
- [ ] Every line explained ([JUDGING.md](../../../JUDGING.md))

## Finished early?
- Add delete, with a confirm step, and say why you would not put it next to Edit in the table.
- Show a "saved" confirmation on the detail page after an edit instead of a silent navigation.
- Stop the API and try to save: make the failure visible to the user rather than a swallowed
  console error.
