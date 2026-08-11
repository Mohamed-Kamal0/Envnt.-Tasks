# Day 6 — JWT auth: login, protect, roles · Catalog tasks

**Start from:** `starter/` — or your own repo if you're current: this starter equals
yesterday's solution plus today's TODO stubs.

Given so today is about **tokens, not plumbing**: the JwtBearer package, the `Jwt` section in
`appsettings.json`, and the auth blocks in `Program.cs` (`AddAuthentication().AddJwtBearer`,
`UseAuthentication()` **before** `UseAuthorization()` — read that order, the review asks
about it), plus Swagger's Authorize button and `Auth/ITokenService.cs`. Yesterday's tests
stay green throughout — auth doesn't touch the service layer.

Yours: `Auth/JwtTokenService.CreateToken`, `AuthController.Login`, and the missing
`[Authorize]` attributes on `ProductsController`.

## Before you start

- [ ] `dotnet test` in `starter/` is green (4/4) — it must still be green when you finish.
- [ ] `dotnet run` works; Swagger shows an Authorize button.

## Tasks

### 1 · Read the given wiring  ⏱ ~10
`Program.cs` auth blocks + the `Jwt` config section. Answer without looking: why must
`UseAuthentication()` run before `UseAuthorization()`? What breaks if they're swapped —
and does it break loudly or **silently**?
- **Done when:** you can answer both, and say what a 401 means vs a 403, one sentence each.

### 2 · `CreateToken`  ⏱ ~15
Implement `Auth/JwtTokenService.CreateToken`: read the `Jwt` config section, build a
`SymmetricSecurityKey` + `SigningCredentials` (HmacSha256), add `Name` and `Role` claims,
create a `JwtSecurityToken` (issuer, audience, claims, expiry), return
`new JwtSecurityTokenHandler().WriteToken(token)`.
- Hint: the role must be a claim **before** the token is signed — adding it afterwards does
  nothing. If `AddJwtBearer` complains about key length: HMAC-SHA256 wants ≥ 16 bytes.
- **Done when:** it compiles; task 3 proves it end to end.

### 3 · `Login`  ⏱ ~10
Implement `AuthController.Login`: `admin`/`password` → token with role `"Admin"`;
`user`/`password` → role `"User"`; anything else → `Unauthorized()` — a **401**, never
"200 with an empty body". (The hard-coded check is deliberate — today teaches tokens, not
credential storage; the comment above the method says what a real app does.)
- **Done when:** `POST /api/auth/login` returns a token for good credentials and 401 for bad
  ones; paste the token at jwt.io (throwaway demo token only) and find your role claim.

### 4 · Protect writes; Admin for delete  ⏱ ~5
`ProductsController`: reads stay public — add `[Authorize]` to `Create` **and** `Update` (the
PUT), and `[Authorize(Roles = "Admin")]` to `Delete` (the three TODO markers show where).
Editing is a write like any other: a `PUT` with no token must answer 401, not 200.
- **Done when:** it compiles; the status-code drill in task 5 is the proof.

### 5 · The status-code drill  ⏱ ~10
Prove the whole policy with curl or Swagger's Authorize button — all five, on purpose:
`GET` with no token → **200** · `POST` with no token → **401** · `POST` with a token →
**201** · `DELETE` as `user` → **403** · `DELETE` as `admin` → **204**.
- **Done when:** you've produced all five codes and `dotnet test` is still 4/4 green.

## Verify

```bash
cd starter
dotnet test                                                   # still 4/4 green
cd CatalogApi && dotnet run &
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5144/api/products                      # 200 (no token)
curl -s -X POST http://localhost:5144/api/auth/login -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"wrong"}' -o /dev/null -w "%{http_code}\n"                 # 401
TOKEN=$(curl -s -X POST http://localhost:5144/api/auth/login -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"password"}' | sed 's/.*"token":"\([^"]*\)".*/\1/')
curl -s -o /dev/null -w "%{http_code}\n" -X POST http://localhost:5144/api/products \
  -H "Content-Type: application/json" \
  -d '{"name":"Refactoring","price":40.00,"inStock":true,"categoryId":1}'                       # 401 (no token)
curl -s -o /dev/null -w "%{http_code}\n" -X POST http://localhost:5144/api/products \
  -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
  -d '{"name":"Refactoring","price":40.00,"inStock":true,"categoryId":1}'                       # 201
USERTOKEN=$(curl -s -X POST http://localhost:5144/api/auth/login -H "Content-Type: application/json" \
  -d '{"username":"user","password":"password"}' | sed 's/.*"token":"\([^"]*\)".*/\1/')
curl -s -o /dev/null -w "%{http_code}\n" -X DELETE http://localhost:5144/api/products/1 \
  -H "Authorization: Bearer $USERTOKEN"                                                         # 403 (User, not Admin)
curl -s -o /dev/null -w "%{http_code}\n" -X DELETE http://localhost:5144/api/products/1 \
  -H "Authorization: Bearer $TOKEN"                                                             # 204 (Admin)
```

---

`solution/` is for **after** an honest attempt — manual-first, AI explains only
([JUDGING.md](../../../JUDGING.md)).
