# Day 3 — REST shape: DTOs, validation, status codes · Catalog tasks

**Start from:** `starter/` — or your own repo if you're current: this starter equals
yesterday's solution plus today's TODO stubs. (The `CatalogConsole/` reference is **dropped
from today on** — its job ended when the API stood up. Days 1–2 keep a copy if you miss it.)

New and given — read before you code: `Dtos/CatalogDtos.cs` (the records exist),
`Validation/CreateProductRequestValidator.cs` (the class exists, rules are yours), Swagger wired
in `Program.cs`, and the service's new `GetProductAsync`/`CreateProductAsync`/`DeleteProductAsync`
(given — today is about the REST boundary, not the queries).

## Before you start

- [ ] `dotnet run` in `starter/CatalogApi` works; Swagger loads at
      `http://localhost:5144/swagger`.
- [ ] You can state yesterday's DI answer from memory: what does Scoped mean?

## Tasks

### 1 · Validation rules  ⏱ ~10
In `Validation/CreateProductRequestValidator.cs`: `Name` non-empty with max length 120, `Price`
greater than 0.
- **Done when:** the validator compiles; you'll see it bite in task 3.

### 2 · `GetById` — 200 or 404, never 200-with-null  ⏱ ~10
Implement `GET /api/products/{id}`: found → `Ok(ToDto(product))`, missing → `NotFound()`.
- Hint: returning `null` from an action is a silent 204 — the classic trap.
- **Done when:** an existing id curls to 200 + JSON, id 999 curls to **404**.

### 3 · `Create` — validate, then 201 + Location  ⏱ ~15
Implement `POST /api/products`: run the validator — invalid → **400** with the field errors;
valid → create, then `CreatedAtAction(nameof(GetById), ...)` so the response is **201** with
a `Location` header pointing at the new product. While you're in the file: switch the list `GET`
to return `ProductDto`s (the boundary rule — entities never cross the door).
- **Done when:** an empty-name POST returns 400 naming `Name`; a valid POST returns 201,
  a `Location` header, and the created product as a DTO.

### 4 · `Delete` — 204 or 404  ⏱ ~5
Implement `DELETE /api/products/{id}`: deleted → `NoContent()`, missing → `NotFound()`.
- **Done when:** deleting an existing id returns **204** and a second delete of the same id
  returns **404**.

### 5 · Prove it in Swagger + curl  ⏱ ~10
Open `/swagger`: all endpoints documented. Then produce all four status codes on purpose
with curl — 200, 404, 201, 400 — and note the exact commands; the review asks for them live.
- **Done when:** you've seen all four codes in your own terminal.

## Verify

```bash
cd starter/CatalogApi
dotnet build                                                        # 0 errors
dotnet run &
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5144/api/products/1     # 200
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5144/api/products/999   # 404
curl -s -o /dev/null -w "%{http_code}\n" -X POST http://localhost:5144/api/products \
  -H "Content-Type: application/json" -d '{"name":"Refactoring","price":40.00,"inStock":true}'   # 201
curl -s -o /dev/null -w "%{http_code}\n" -X POST http://localhost:5144/api/products \
  -H "Content-Type: application/json" -d '{"name":"","price":0,"inStock":true}'                   # 400
curl -s -o /dev/null -w "%{http_code}\n" -X DELETE http://localhost:5144/api/products/1           # 204
```

---

`solution/` is for **after** an honest attempt — manual-first, AI explains only
([JUDGING.md](../../../JUDGING.md)).
