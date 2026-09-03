# Lab 2 — Context lab (the right page)

> **Run this against your OWN app.** Wherever a step names a bench file (`web/src/app/product.service.ts`, `api/CatalogApi/Services/CatalogService.cs`, …), use your own repo and your own file — this day's [bench](..) is the ready-made .NET + Angular scenario if your app won't start.

**Pillar:** Context · **Time:** ~20 min · **Runs against:** your own app

Most "the AI is dumb" moments are really "it couldn't see the right thing." You'll create one on
purpose, then fix it.

---

## Drill A — point, don't dump (7 min)

First, **plant a bug**: in `web/src/app/product.service.ts` (or wherever your loading state lives),
change `this.loading.set(false)` to `this.loading.set(true)` inside the `next:` callback of
`load()`. Save — the page now shows "Loading products…" forever, even though the request succeeded.

**1. Weak** (no pointer — watch it wander):

```
my app is broken, the products never show up. fix it
```

Note what it does: which files did it open? how long did it take? did it guess?

**2. Strong** (point at the page):

```
In web/src/app/product.service.ts the "Loading products…" message never goes away, even though
the Network tab shows the GET /api/products returning 200. Look at load() and the loading signal.
Why, and what's the one-line fix?
```

**3. Compare** speed and confidence. Same brain, same bug — different desk.

## Drill B — give it memory (8 min)

Create a memory file at the root of the bench (or your own repo). The name depends on your tool:

| tool | file |
|---|---|
| Claude Code | `CLAUDE.md` at the repo root |
| Codex | `AGENTS.md` at the repo root |
| **GitHub Copilot** (VS Code) | `.github/copilot-instructions.md` |

Same idea in all three: rules the tool reads at the start of every session, so you stop retyping
them. It has to cover **both** halves of the stack:

```markdown
# Project conventions
- .NET 10 Web API in api/, Angular 18 standalone app in web/. No new NuGet or npm packages.
- Controllers stay thin: request in, service call, DTO out, status code. No query logic.
- Entities never cross the boundary — everything returned is a record DTO from Dtos/.
- Angular: one component per file, kebab-case filenames; only services touch HttpClient.
- Prices are numbers; display them with .toFixed(2).
- After any change, the app must still: show "Loading products…", then render all products.
```

Now run — **without repeating any of those rules**:

```
add a "NEW" badge to the product card for products with id >= 7
```

**Check:** did it follow the file layout, naming, and CSS conventions *without being told in the
prompt*? That's memory doing the work your typing used to do — on every future prompt, for free.

## Drill C — search over paste (5 min)

Old habit: paste a whole file and ask about it. New habit — make the AI *find* it:

```
Search this project for every place a product's price is read or formatted — C# and TypeScript
both — and list file + line + what it's used for there. Don't show me the file contents, just
the map.
```

You got the *conclusion* without filling the window with code. On a big repo, this is the
difference between a focused answer and a stuffed, foggy one.

---

## Done when
- The weak run of Drill A visibly wandered (or took longer) vs. the pointed run.
- Drill B's component followed your conventions **without them in the prompt**.
- You can say the mantra: **curate · cache · remember** — and give one example of each.

**Log line:** *"With a memory file in place, I stopped having to repeat ___."*
