# Day 15 Starter — Catalog (.NET API + Angular)

The clean, working **.NET 10 API + Angular 18** bench — search, sort, cards. Your starting point
for the full-day build.

> You may use your OWN Week 1–2 app for today's lab instead. This bench is the fallback.

## Run it

```bash
cd api/CatalogApi && dotnet run      # http://localhost:5144
cd web && npm install && npm start   # http://localhost:4200
```

## What today's lab does with it

Day 15 is the **Full AI Day** — one feature, start to finish, the disciplined way.

1. Read [`FEATURE.md`](FEATURE.md): a shopping cart — add-to-cart, a count badge, a total. It
   crosses **both** halves of the stack, which is what makes the planning matter.
2. **Session 1 — THINK:** in one AI session, produce `plan.md` and write **no application code**.
   Name the endpoints, the DTOs, the service, the components; order the steps; say how you'll
   verify each.
3. **Session 2 — BUILD:** open a *fresh* session, hand it `plan.md`, and build.
4. **Present** in the 4-part format: what you asked for, what the AI produced, what you changed or
   rejected, what you learned.

The point is the two-session split — planning before building — not the cart itself.
Full task sheet: [../../student-tasks.md](TASKS.md).

## Files

- `FEATURE.md` — the feature to plan, then build
- `api/CatalogApi/` — controllers, services, DTOs; the cart's server half goes here
- `web/src/app/` — the Angular half: `product.service.ts`, `product-list.ts`, `product-card.ts`, `app.ts`
