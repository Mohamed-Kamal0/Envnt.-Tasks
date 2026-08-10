# Day 5 — Service hardening + first tests · Catalog tasks

**Start from:** `starter/` — or your own repo if you're current: this starter equals
yesterday's solution plus today's refactor scaffold and a ready-made test project.

What changed under you (given — read it first, it IS the lesson):

- The seam grew up: `ICatalogService` now returns **DTOs, not entities** — projection happens
  inside the query now — and every method takes a **`CancellationToken`** threaded down into
  EF Core. (The seam was already `ICatalogService`, so there's no rename today — only the
  signature reshape.)
- `GetProductsAsync` is implemented as the worked example of that final shape. The other four
  methods are stubs that throw — your day-4 bodies are the raw material; reshaping them is
  today's refactor.
- `CatalogApi.Tests/` is wired (xUnit + EF Core InMemory + project reference) and its 4 tests
  are **complete — read them, don't edit them.** They are your acceptance criteria, red on
  purpose.

## Before you start

- [ ] `dotnet build` succeeds from `starter/` (the solution folder).
- [ ] `dotnet test` runs: **1 green** (`GetProducts_filters_by_category` — the worked
      example), **3 red** with `NotImplementedException`. That's the correct starting state.

## Tasks

### 1 · Read the seam and the tests  ⏱ ~10
Read `ICatalogService`, the worked `GetProductsAsync`, and all of `CatalogServiceTests.cs`.
Notice what each test constructs: a **fresh in-memory database per test** — no SQLite file,
no shared state. The interface is what makes that swap possible.
- **Done when:** you can name the three parts of one test (Arrange/Act/Assert) and answer:
  why does `NewDb()` use `Guid.NewGuid()` as the database name?

### 2 · The refactor — four methods, tests going green  ⏱ ~30
Implement `GetProductAsync`, `CreateProductAsync`, `DeleteProductAsync`, `GetCategoriesAsync` in
`Services/CatalogService.cs`, in the worked example's shape: project to the DTO **inside**
the query (`Select(p => new ProductDto(...))`), pass `ct` to every EF call
(`ToListAsync(ct)`, `FirstOrDefaultAsync(ct)`, `SaveChangesAsync(ct)`…). Run `dotnet test`
after each method — watch them go green one by one.
- Hint: each stub's TODO comment names the exact LINQ chain. `GetProductAsync` on a missing id
  returns `null` — no exception; the controller turns it into the 404.
- **Done when:** `dotnet test` reports **4/4 green**.

### 3 · "How do you know you didn't break it?"  ⏱ ~5
The mentor question of the day. Answer it twice: run the API and curl yesterday's checks
(200/404/201/204, `?category=`) — and then say why the **tests** are the better answer to
the question than the curls.
- **Done when:** curls pass against the running API and you can defend the refactor
  rule-by-rule against the six clean-code habits.

## Verify

```bash
cd starter
dotnet build          # 0 errors
dotnet test           # 4/4 green
cd CatalogApi && dotnet run &
curl -s "http://localhost:5144/api/products?category=Books"      # 2 products, DTO shape with category names
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5144/api/products/999   # 404
```

## Finished early?

- Add `GET /api/products?search=<term>` — case-insensitive match on `Name`, only when `search`
  is present — plus one test of your own proving it. (Your build; the solution stays
  search-free on purpose.)

---

`solution/` is for **after** an honest attempt — manual-first, AI explains only
([JUDGING.md](../../../JUDGING.md)).
