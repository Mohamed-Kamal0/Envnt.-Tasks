# Day 4 — EF Core + SQLite + migration · Catalog tasks

**Start from:** `starter/` — or your own repo if you're current: this starter equals
yesterday's solution plus today's TODO stubs.

Given so you don't fight tooling: the NuGet packages (Sqlite, Design, and the
`SQLitePCLRaw` bundle) are already in the `.csproj`; `Data/DbSeeder.cs` and
`Data/AppDbContextFactory.cs` are complete (read the factory's comment — it answers "why
does `dotnet ef` work without starting the app?"); `Product` already carries its `CategoryId`
foreign key + navigation property as the worked half of the relationship. The filter also grew
up — the controller now takes `?category=<name>` instead of the old `?inStock=`. There is **no
migration yet — creating it is your task.**

## Before you start

- [ ] Yesterday's endpoints still pass their 200/404/201/400 checks.
- [ ] `dotnet ef --version` works (`dotnet tool install -g dotnet-ef` if not).

## Tasks

### 1 · Finish the relationship  ⏱ ~10
`Models/Category.cs`: add the "many" side navigation property (`List<Product> Products`,
initialized). Then `Data/AppDbContext.cs`: declare the one-to-many explicitly in
`OnModelCreating` — `HasOne`/`WithMany`/`HasForeignKey`.
- **Done when:** it compiles and you can point at each side of the relationship and
  describe it in words ("one category has many products; a product has exactly one category").

### 2 · Register the context  ⏱ ~5
`Program.cs`: uncomment the `AddDbContext` block. Find the connection string it reads and
say where the `.db` file will appear.
- **Done when:** `dotnet build` still succeeds with the block active.

### 3 · The migration  ⏱ ~10
```bash
dotnet ef migrations add InitialCreate
```
Read the generated `Up()` — find your two tables, the foreign key, and say what `Down()`
would do. Then uncomment the migrate-and-seed block in `Program.cs`.
- **Done when:** a `Migrations/` folder exists and you can read `Up()` aloud; `dotnet run`
  creates `catalog.db` and seeds it (2 categories, 3 products).

### 4 · Swap the service to real queries  ⏱ ~20
`Services/CatalogService.cs`: retire the static lists. Inject `AppDbContext`, then rewrite each
method as an EF query following the per-method TODO hints — `Include(p => p.Category)` on the
reads, `SaveChangesAsync` on the writes, `GetCategoriesAsync` projecting straight to `CategoryDto`.
Every call awaited: no `.Result`, no `.Wait()`.
- Hint: creating a migration doesn't apply it — startup's `Migrate()` (or
  `dotnet ef database update`) does.
- **Done when:** all of yesterday's checks pass **against the file database** — including
  `?category=`, and a product you POST survives an app restart (that's the whole point).

## Verify

```bash
cd starter/CatalogApi
dotnet build                                                  # 0 errors
dotnet ef migrations list                                     # InitialCreate
dotnet run &
curl -s http://localhost:5144/api/products                    # 3 seeded products, category names filled
curl -s "http://localhost:5144/api/products?category=Books"   # 2 products
curl -s http://localhost:5144/api/categories                  # 2 categories with product counts
curl -s -o /dev/null -w "%{http_code}\n" -X POST http://localhost:5144/api/products \
  -H "Content-Type: application/json" \
  -d '{"name":"Refactoring","price":40.00,"inStock":true,"categoryId":1}'   # 201
# restart the app — the POSTed product is still there. In-memory could never do that.
```

---

`solution/` is for **after** an honest attempt — manual-first, AI explains only
([JUDGING.md](../../../JUDGING.md)).
