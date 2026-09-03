# Day 2 — First API: DI + LINQ · Catalog tasks

**Start from:** `starter/` — or your own repo if you're current: this starter equals
yesterday's solution (your console lives on in `CatalogConsole/` for reference) plus a fresh
`CatalogApi/` web project with today's TODOs.

## Before you start

- [ ] Your day-1 console runs (or `starter/CatalogConsole` does).
- [ ] `cd starter/CatalogApi && dotnet run` starts; `curl http://localhost:5144/api/products`
      returns 3 seeded products.

## Tasks

### 1 · Read the wiring — where's `new`?  ⏱ ~10
Read `Program.cs`, `Services/ICatalogService.cs`, and the controller's constructor. Nobody ever
writes `new CatalogService()` — find who does it instead, and what **Scoped** means for how
often it happens.
- **Done when:** you can answer: who constructs `ProductsController`? Who hands it the service?
  How many `CatalogService` instances exist across two parallel requests?

### 2 · Wire `?inStock=` through the controller  ⏱ ~10
In `Controllers/ProductsController.cs`: accept `[FromQuery] bool? inStock` and pass it to
`GetProductsAsync(inStock)` instead of `null`.
- Hint: `bool?` stays `null` when the parameter isn't in the URL — that's the "not asked"
  signal the service checks.
- **Done when:** `curl "http://localhost:5144/api/products?inStock=true"` reaches your service
  with `true` (a breakpoint or a `Console.WriteLine` proves it).

### 3 · The LINQ filter in the service  ⏱ ~15
In `Services/CatalogService.cs`: when `inStock` has a value, filter the list on it before
ordering. Work out the chain yourself — the operator you need was on slide 4.
- Hint: LINQ returns a **new** sequence and never mutates the list, so the result has to
  be assigned or returned. A chain that runs and changes nothing is the classic day-2 bug.
- **Done when:** `?inStock=true` returns 2 products, `?inStock=false` returns 1, and no
  parameter returns all 3.

### 3b · Write the chains yourself  ⏱ ~20
Still in `CatalogService.cs`, add a scratch method and write each of these as a **single
method-syntax chain**. Print them from `Program.cs` (or set a breakpoint) to check.
Method syntax only — `from … select` was name-dropped, not taught.

- [ ] **a.** Every product that is in stock.
- [ ] **b.** Just the **names**, as a `List<string>`.
- [ ] **c.** Products under **$50**, cheapest first.
- [ ] **d.** The **first** product in `"Books"`, or `null` when there is none.
      *(Careful: one of the two obvious operators throws instead of returning null.)*
- [ ] **e.** **Is there any** product over `$100`? — a `bool`.
- [ ] **f.** **How many** in-stock products are in `"Electronics"`? — an `int`.
- [ ] **g.** The **names** of in-stock `"Books"`, alphabetical.

- **Done when:** all seven compile and print what you expect against the seeded catalog,
  and you can say why `.Select(p => p.Name).Where(p => p.InStock)` would *not* compile.

### 4 · Break it on purpose  ⏱ ~10
Request `?inStock=banana`. You get a structured **400** with a readable message — not a
500, not a crash. Find out who produced it (hint: the `[ApiController]` attribute's model
binding) and explain why the request never even reached your service.
- **Done when:** you can say in one sentence why a bad query string is the *client's* error
  (400) and where in the pipeline it was caught.

## Verify

```bash
cd starter/CatalogApi
dotnet build                                                    # 0 errors
dotnet run &
curl -s http://localhost:5144/api/products                      # 3 products
curl -s "http://localhost:5144/api/products?inStock=true"       # 2 products
curl -s "http://localhost:5144/api/products?inStock=false"      # 1 product
curl -s -o /dev/null -w "%{http_code}\n" "http://localhost:5144/api/products?inStock=banana"   # 400
```

---

`solution/` is for **after** an honest attempt — manual-first, AI explains only
([JUDGING.md](../../../JUDGING.md)).
