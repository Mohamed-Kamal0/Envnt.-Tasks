# Student tasks — Week 2 · Day 7: Components & Pages

**Today's goal:** get the TypeScript that Angular is written in solid, then build the ShopEase
shell out of standalone components you write yourself — a navbar, a footer, a hero banner — and
compose them into four pages. **You'll need:** Node 18+, an editor, and this day's `starter`.

**Manual-first:** weeks 1–2 are hands-by-you. AI may explain a concept or an error message — it
does not write your components. Any line you can't explain, you redo (the
[JUDGING.md](../../../JUDGING.md) rule).

**Reference solution:** after you've done the manual-first work, check yours against `solution`
(`starter` is where you begin).

## Before you start
- [ ] Node 18+ installed (`node -v`).
- [ ] `cd starter && npm install && npm start` runs, and the browser shows **missing-module
      errors** for the components you are about to build — that is the correct starting state.
- [ ] You've read the `#Task` comments at the top of `src/app/app.component.ts`. They are the
      brief for the whole day; each stub repeats its own.

## TypeScript warm-up (first ~35 min)

Angular components are TypeScript. Before the markup, get the language reflexes down in one small
file you check by compiling — no Angular yet. These types then feed the components you build below,
so this is not throwaway practice.

### A · Interfaces, unions, and a typed function  ⏱ ~15
Create `src/app/ts-warmup.ts` and export four things, no `any` anywhere:
- an `interface NavLink { label: string; path: string }`,
- a union/literal type `type Badge = 'new' | 'sale' | 'none'`,
- a typed function `currentYear(): number` returning this year,
- a `const shopLinks: NavLink[]` holding the four ShopEase links (Home, Shop, About Us, Contact
  Us) with `path` values like `'/'`, `'/shop'`, `'/about-us'`, `'/contact-us'`.
**Done when:** `cd starter && npx tsc --noEmit` reports zero errors — and changing one link's
`path` to a number, or setting a `Badge` to `'discount'`, turns red. Undo those after you've seen
it.
Stuck? A union type is the whole set of allowed values (`'new' | 'sale' | 'none'`) — the compiler
rejects anything outside it, which is the point.

### B · One generic  ⏱ ~10
In the same file, write `first<T>(items: T[]): T | undefined` returning the first element (or
`undefined` for an empty array). Call it once on `shopLinks` and once on a `number[]`.
**Done when:** hovering the two calls shows `NavLink | undefined` and `number | undefined` — one
function, the compiler inferring `T` at each call site. That inference is why generics beat writing
`firstLink()` and `firstNumber()` by hand.

### C · Put the types to work as you build  ⏱ ~10
Import these into the components in the next section instead of loose values:
- the footer's year comes from `currentYear()`, not a literal;
- the navbar keeps its four links in a `readonly NavLink[]` on the class — today the template still
  lists the four anchors by hand (no `@for` until day 8), but the typed array is the source of
  truth you'll render from tomorrow;
- the hero's badge value is typed `Badge`, so a typo can't slip through.
**Done when:** none of the three components has an `any` or a magic string where one of your
warm-up types belongs.

## Tasks

### 1 · Read the brief before writing anything  ⏱ ~10
Open `src/app/app.component.ts` and read all seven `#Task` comments, then look at the `.css` files
already sitting in `components/navbar`, `components/footer` and `components/hero`. The class names
in that CSS tell you what markup is expected.
**Done when:** you can say which three components have no `.ts`/`.html` yet, and which four pages
are stubs.

### 2 · The navbar  ⏱ ~25
Create `components/navbar/navbar.component.ts` + `.html` (`ng generate component
components/navbar --standalone`, or by hand — do **not** recreate the `.css`). A brand link on the
left reading **ShopEase**, and four plain links on the right: Home, Shop, About Us, Contact Us.
They stay `href="#"` placeholders today; day 8 turns them into real routes.
**Done when:** the navbar renders with its provided styling and all five links are visible.
Stuck? A standalone component needs `standalone: true` and its own `imports` array — nothing is
registered globally any more.

### 3 · The footer  ⏱ ~15
Create `components/footer/footer.component.ts` + `.html`. Muted credit text on the left —
"ShopEase · built in this Angular course" — and a copyright line on the right showing the current
year from a class property, not typed in by hand.
**Done when:** the year in the browser changes if you change your machine's clock, because it came
from `new Date()`.

### 4 · The hero banner  ⏱ ~20
Create `components/hero/hero.component.ts` + `.html`: a "New season" badge, the headline
**"Everything you need, one cart away."**, a short paragraph, and a "Shop now" call to action.
**Done when:** the hero fills the top of the page and the provided CSS classes all have markup to
attach to.

### 5 · Compose the home page  ⏱ ~20
Fill in `pages/home/home.component.ts` + `.html`: import your three components plus the existing
`ProductsComponent`, and place them in order — navbar, hero, products, footer.
**Done when:** `/` shows the whole landing page, and removing one import breaks exactly one part
of it (try it, then put it back).

### 6 · About Us and Shop  ⏱ ~25
Fill in `pages/about-us` (an "About Us" heading and a paragraph about ShopEase) and `pages/shop`
(a "Shop" heading, and the existing `<app-products />` reused beneath it).
**Done when:** both compile, and the Shop page reuses the products component rather than copying
its markup.
Stuck? Reusing a component means importing it — not pasting its template.

### 7 · The contact form  ⏱ ~25
Fill in `pages/contact-us`: a heading and a compact form — an email field, a multi-line message
field, and a "Send" button. Nothing is wired up today; it submits nowhere on purpose.
**Done when:** the form renders and the button is clickable, with no console errors.

## Verify

```bash
cd starter
npm start          # no missing-module errors left
```

Then in the browser: the landing page shows navbar → hero → products placeholder → footer, in
that order.

## End-of-day deliverables
- [ ] `ts-warmup.ts` compiles clean (`npx tsc --noEmit`): a `NavLink` interface, a `Badge` union, a
      typed `currentYear()`, and a `first<T>` generic — no `any`
- [ ] Those types are actually used: the footer year from `currentYear()`, the navbar links as a
      typed `NavLink[]`, the hero badge typed `Badge`
- [ ] `NavbarComponent`, `FooterComponent`, `HeroComponent` written by you, each standalone, each
      using the CSS that was already there
- [ ] All four pages compile: home composes the shell, shop reuses `<app-products />`
- [ ] The footer's year comes from code, not a literal
- [ ] `npm start` runs clean — no missing modules, no console errors
- [ ] Every line explained ([JUDGING.md](../../../JUDGING.md))

## Finished early?
- Pull the four navbar links out into an array on the class and render them with `@for`. Day 8
  will thank you.
- Add a second hero variant and switch between them with `@if` on a boolean.
- Read `app.component.ts` and answer, in one sentence, why `About Us` exists as a file today but
  cannot be reached in the browser.
- More TypeScript: give `NavLink` an optional `badge?: Badge`, then narrow it — write a function
  that takes a `NavLink` and returns different text for each `Badge` value, and let the compiler
  prove you handled every case (a `switch` with no `default`, or an exhaustiveness check).

