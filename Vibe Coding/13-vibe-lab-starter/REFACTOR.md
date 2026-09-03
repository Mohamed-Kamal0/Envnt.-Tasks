# Refactor — extract the filter, behind a test

A **guarded refactor**: change how the code is organized without changing what it does — and
prove that with a test, not a glance.

## The target

`api/CatalogApi/Services/CatalogService.cs` filters the catalog inline, in the middle of
`GetProductsAsync`:

```csharp
if (!string.IsNullOrWhiteSpace(search))
    query = query.Where(p => p.Name.Contains(search, StringComparison.Ordinal));
```

That's fine at this size. It stops being fine the moment the filter grows (multiple fields, a
category filter tomorrow, ranking) — and today it can't be tested without standing up the whole
service.

**Read that predicate carefully before you touch anything.** It is `StringComparison.Ordinal`:
the search is **case-sensitive**. Searching "Mouse" finds a product; "mouse" finds nothing. That
is today's behavior, quirk and all.

## The refactor

Extract it into a pure, static function — `ProductFilter.ByName(products, search)` in a new
`api/CatalogApi/Filtering/ProductFilter.cs`. `CatalogService` calls it instead of filtering inline.

## Do it guarded — ask for the test first

Before you let anything touch `CatalogService.cs`, ask your AI pair for the **tests**, not the code:

```
In api/CatalogApi, I want to extract the inline name filter from
Services/CatalogService.cs into a pure static ProductFilter.ByName(IEnumerable<Product>, string?)
in Filtering/ProductFilter.cs. Behavior must stay EXACTLY as it is today, including the
case-sensitive Ordinal comparison and blank-search-returns-everything.

First: write 4 xUnit tests in CatalogApi.Tests/ProductFilterTests.cs that describe the CURRENT
behavior — blank/null search, a substring that matches one product, a lowercase query that
matches nothing, and a shared substring that preserves the original order.
Show me the tests and stop. Do not touch CatalogService.cs yet.
```

Read the tests. Do they describe what the code does *today*, or what the AI thinks it should do?
Then give the go-ahead for the extraction.

## Constraints

- **No new packages** — NuGet or npm. xUnit is already wired in `CatalogApi.Tests`.
- Behavior identical before and after: same search string in, same products in the same order out.
  The case-sensitivity **stays**.
- The controller doesn't change. This is a service-layer refactor.

## The trap (this is the actual exercise)

Your AI pair will want to "improve" the case-sensitive match to `OrdinalIgnoreCase` while it's in
there. That is a behavior change wearing a refactor's clothes, and it will pass a casual review
because it looks like a fix. Catch it in the diff, and say why in your lab log: *a refactor that
changes behavior isn't a refactor — it's an unreviewed feature.* Making the search
case-insensitive is a fine next ticket, with its own test change.

## Done when

- `Filtering/ProductFilter.cs` exists and `CatalogService` calls it instead of filtering inline.
- `dotnet test` is green: the smoke test plus your 4 new tests.
- `curl "http://localhost:5144/api/products?search=Mouse"` returns one product and
  `?search=mouse` returns `[]` — same as before the refactor.
- You can point at the tests and say "this is what proves I didn't break it" — not "it looked
  fine when I clicked around."

## Before you start: your CLAUDE.md

Write a `CLAUDE.md` (or `AGENTS.md`) at the root of `starter/` — two minutes, not a document
project. 4–6 **checkable** conventions covering both halves: where pure logic lives, what stays
out of the controller, what the Angular service owns, "no new packages", how errors surface. Then
run the refactor and watch whether the AI follows the file without you repeating it in the prompt.
See [lab/01-context-lab.md](lab/01-context-lab.md) Drill B for the recipe.
