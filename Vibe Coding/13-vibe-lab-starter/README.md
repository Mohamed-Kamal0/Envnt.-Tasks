# Day 13 Starter — Catalog (.NET API + Angular)

The **.NET 10 API + Angular 18** bench, working, plus an xUnit test project that is green and
nearly empty — today you fill it.

> You may use your OWN Week 1–2 app for today's lab instead. This bench is the fallback.

## Run it

```bash
cd api && dotnet test          # 1 passing smoke test — your baseline
cd api/CatalogApi && dotnet run
```

```bash
cd web && npm install && npm start
```

## What today's lab does with it

Day 13 is **Context — feed it the right page**. Two things, in order:

1. **Start a `CLAUDE.md`** (or `AGENTS.md`) at the root of this folder — a handful of checkable
   conventions covering both halves of the stack, not a wish list. See
   [lab/01-context-lab.md](lab/01-context-lab.md) Drill B.
2. **One guarded refactor.** Open [`REFACTOR.md`](REFACTOR.md): pull the inline search filter out
   of `CatalogService` into a pure, tested function — **write the test first**, then refactor,
   then prove the test still passes.

Full task sheet: [../../student-tasks.md](TASKS.md).

## Files

- `REFACTOR.md` — the guarded refactor to run today
- `api/CatalogApi/Services/CatalogService.cs` — the inline filter you'll extract
- `api/CatalogApi.Tests/` — xUnit, one smoke test, wired to the API project
- `web/src/app/` — the Angular half: service, list, card
