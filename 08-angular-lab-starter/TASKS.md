# Student tasks — Week 2 · Day 8: Routing & Mock Products

**Today's goal:** give ShopEase real URLs — a routes table, routed navbar links, one shell around
a single `<router-outlet>` — put the first data on screen with `@for`, then hold that data back
until it's needed with `@defer`. **You'll need:** this day's `starter` (day 7's app plus today's
gaps).

**Manual-first:** weeks 1–2 are hands-by-you. AI may explain a concept or an error message — it
does not write your routes or your template. Any line you can't explain, you redo (the
[JUDGING.md](../../../JUDGING.md) rule).

**Reference solution:** after you've done the manual-first work, check yours against `solution`
(`starter` is where you begin).

## Before you start
- [ ] `cd starter && npm install && npm start` runs, and clicking the navbar links does nothing —
      they are still `href="#"` placeholders.
- [ ] You've skimmed the `#Task` comments in `navbar.component.*`, `app.component.ts` and
      `products.component.*`.

## Tasks

### 1 · The Product model  ⏱ ~10
Create `src/app/models/product.ts` by hand — a plain file, not `ng generate`. The `Product`
interface needs a numeric id, a title, a numeric price, a SKU string and a description.
**Done when:** the interface compiles and nothing imports it yet.

### 2 · The routes table  ⏱ ~30
Create `src/app/app.routes.ts` from scratch: a `Routes` array with Home, Shop, About Us and
Contact Us pointing at their page components, plus a wildcard that redirects anything unknown back
to home. Then create `src/app/app.config.ts` and register it with `provideRouter`.
**Done when:** typing `/shop` in the address bar loads the shop page, and `/nonsense` bounces you
to home.
Stuck? A wildcard (`**`) must be the LAST entry — the router takes the first match it finds.

### 3 · Routed navbar links  ⏱ ~20
In `navbar.component.ts`, import the router-link directives; in the template, turn the four
anchors into routed links and highlight whichever one matches the current page.
**Done when:** clicking a link changes the URL with no full page reload (the network tab stays
quiet), and the active link is styled — `.nav-link.active` is already in `styles.css`.

### 4 · One shell, one outlet  ⏱ ~15
In `app.component.ts`, import the router outlet and place it between the navbar and the footer, so
every page renders inside one shell instead of each page repeating it.
**Done when:** the navbar and footer stay put while the middle of the page changes as you
navigate.

### 5 · Mock products  ⏱ ~15
In `products.component.ts`, fill the `products` array with at least six `Product` objects.
**Done when:** the array is typed as `Product[]` and the compiler complains if you leave a field
out.

### 6 · The product grid  ⏱ ~30
In `products.component.html`, render the list with `@for` (and `@empty` for the nothing-to-show
case). Give every card an even/odd modifier class driven by the product's id — `.even` and `.odd`
are already styled.
**Done when:** six cards render in alternating colours, and emptying the array shows the `@empty`
message instead of a blank strip.
Stuck? `@for` needs a `track` expression — use the product's id.

### 7 · Defer the products until they're on screen  ⏱ ~25
The product grid sits below the hero, so there's no reason to build it before the visitor scrolls
to it. In `pages/home/home.component.html`, wrap `<app-products />` in an `@defer` block that loads
`on viewport`, with a `@placeholder` (a simple "Products loading…" box — the `@for` grid is what's
being deferred, so the placeholder is what shows first) and a `@loading` block.
**Done when:** on a hard refresh the placeholder shows first, and `ProductsComponent` only
initialises when you scroll it into view — prove it with a `console.log` in the products
constructor that fires on scroll, not on load. Remove the log after.
Stuck? `@defer` only defers a **standalone** component's load — `ProductsComponent` already is one,
which is exactly what makes it eligible; a plain `<div>` of markup can't be deferred the same way.

### 8 · A second trigger, and `@error`  ⏱ ~15
Add an `@error` block to the same `@defer`, then try a different trigger — `on interaction` (loads
when the placeholder is clicked) or `on timer(2s)` (loads after two seconds) — and watch when the
swap happens in each.
**Done when:** you can say in one sentence each what `on viewport`, `on interaction`, `on idle` and
`on timer` mean, and which you'd pick for a below-the-fold product grid (and why it's `on
viewport`).

## Verify

```bash
cd starter
npm start
```

Then: `/`, `/shop`, `/about-us`, `/contact-us` all load, the active link is highlighted, the grid
alternates colours, and on the home page the products come in via the `@defer` placeholder — not on
first paint.

## End-of-day deliverables
- [ ] `models/product.ts`, `app.routes.ts` and `app.config.ts` all written by you
- [ ] Four working routes + a wildcard redirect
- [ ] Navbar uses routed links with an active state; no full page reloads
- [ ] One `<router-outlet>` in the app shell, not a navbar per page
- [ ] Six or more mock products rendered with `@for` / `@empty`, even/odd colouring by id
- [ ] The products deferred with `@defer (on viewport)` — a `@placeholder`, a `@loading` and an
      `@error` block, and the component proven to init on scroll, not on load
- [ ] Every line explained ([JUDGING.md](../../../JUDGING.md))

## Finished early?
- Add a `category` field to `Product` and a filter above the grid.
- Read up on `*ngFor` — the older syntax you will meet in existing codebases — and write two lines
  on how it differs from `@for`.
- Give the wildcard route its own "not found" page instead of a redirect, then argue for one over
  the other.
- Add a `@defer` `prefetch` trigger (e.g. `prefetch on idle`) alongside the load trigger, and
  explain the difference between *prefetching* the code and *rendering* the block.
