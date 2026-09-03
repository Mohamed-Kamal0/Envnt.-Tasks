# Day 14 Starter — Catalog (.NET API + Angular, needs work)

The same **.NET 10 API + Angular 18** bench as yesterday. This copy **does not work
correctly** — that's the point of today.

> You may use your OWN Week 1–2 app for today's lab instead. This bench is the fallback.

## Run it (two terminals)

```bash
cd api/CatalogApi
dotnet run                     # http://localhost:5144 · keep this console VISIBLE
```

```bash
cd web
npm install
npm start                      # http://localhost:4200 · open the browser console first
```

## What today's lab does with it

Day 14 is **The Model Brain** — right-size the model, then use that judgment as a debugging
habit: read the error, form a hypothesis, ask one pointed question, escalate a rung only if the
cheap one has genuinely failed you twice. See [lab/01-brain-ladder.md](lab/01-brain-ladder.md)
Drill D and the full task sheet in [../../student-tasks.md](TASKS.md).

There are **three** defects, one per layer, and they announce themselves differently:

- one **throws** — the API console is where it confesses;
- one is **silently wrong** — a button that does the opposite of its label;
- one **never throws at all** — you'll only find it by reading the code like a reviewer.

For each: read the actual error or behavior → hypothesize in one sentence → ask ONE pointed
question at the cheapest rung that could answer it → escalate only after two bad answers → fix,
confirm, and **log which rung actually fixed it**.

Resist "just fix everything" at the top rung. One issue at a time.

## Files

**API** — `api/CatalogApi/Services/CatalogService.cs` · `Controllers/ProductsController.cs`

**Web** — `web/src/app/product.service.ts` · `product-list.ts` · `app.ts` ·
`web/src/environments/environment.ts`
