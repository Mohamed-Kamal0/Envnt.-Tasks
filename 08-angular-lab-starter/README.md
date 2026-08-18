> 🚧 **This is the STUDENT STARTER version** — routing and the mock product list are intentionally incomplete. Follow the TODO comments in the listed files. See `ecommerce-startup/day-02` for the completed reference solution if you get stuck.
>
> Styling (CSS) is provided for you — your hands-on work is routing, data, and template logic only.

> The Product model, routes config, and app config in this project don't exist yet — you create them from scratch. Everything else already exists from Day 1 and just needs edits.

## 🎯 Your Tasks

- **#Task 1** — `src/app/models/product.ts`: this file does not exist yet — create it from scratch (a plain TypeScript file, not something you `ng generate`). Give the `Product` interface a numeric id, a text title, a numeric price, a text SKU code, and a text description field.
- **#Task 2** — `src/app/app.routes.ts`: this file does not exist yet — create it from scratch. Build the `Routes` array with entries for Home, Shop, About, and Contact, each pointing at its matching page component, plus a wildcard entry that redirects back to home.
- **#Task 3** — `src/app/app.config.ts`: this file does not exist yet — create it from scratch (a plain config file — `ng generate` isn't the right tool here). Register the routing configuration with `provideRouter` in the `providers` array.
- **#Task 4** — `src/app/components/navbar/navbar.component.ts`: import the router-link directives and add them to the component's `imports` array.
- **#Task 5** — `src/app/components/navbar/navbar.component.html`: convert the plain anchor tags into routed links and highlight whichever one matches the current page.
- **#Task 6** — `src/app/app.component.ts`: import the router outlet and place it between the navbar and footer so routed pages render.
- **#Task 7** — `src/app/components/products/products.component.ts`: populate the `products` array with at least 6 mock `Product` objects.
- **#Task 8** — `src/app/components/products/products.component.html`: render the product list with Angular's `@for` / `@empty` control-flow block, giving each card an even/odd modifier class (the colors for `.even`/`.odd` are already styled for you).

# Day 02 — Routing & Mock Products

## Hands-on

1. **Routing Setup** — configure `app.routes.ts`, linking `Home`, `About Us`, `Shop`, `Contact Us`.
2. **Navbar Navigation Hooks** — replace static `<a href="#">` with `routerLink` + `routerLinkActive`.
3. **Mock Array Creation** — `products.component.ts` gets a local array of products (`id`, `title`, `price`, `sku`, `description`).
4. **Conditional Card Styling Challenge** — render with the `@for` control-flow block; colour each card **green** if `id` is even, **blue** if `id` is odd.

## Run it

```bash
npm install
npm start
```

## What changed since Day 1

- `app.routes.ts` + `provideRouter(routes)` in `app.config.ts` — the app now has real URLs.
- **Navbar** and **footer** moved out of `HomeComponent` and into `AppComponent`, wrapping a single `<router-outlet>`. Every page renders inside that outlet, so the shell is built once instead of copy-pasted into every page.
- **Navbar** uses `routerLink` (client-side navigation, no full reload) and `routerLinkActive="active"` (see `.nav-link.active` in `styles.css`) to highlight the current page.
- **ProductsComponent** has real data and a real template: the `@for` block (Angular's built-in control-flow syntax, no `NgFor` import needed) repeats the array, and `[class.even]` / `[class.odd]` pick the background colour per the id-based rule from the hands-on.

## Why `@for` instead of `*ngFor`

Both work. `@for` is the newer block syntax (Angular 17+) and needs nothing in the component's `imports` array, which is why it was chosen here — one less import to explain. `*ngFor` (used in the generic-course `day-02-binding-directives` project) is still what you will see in most existing codebases; know both.

## Try it yourself

1. Add a `category` field to `Product` and a category filter above the grid.
2. Give `ProductsComponent` a third colour rule: ids divisible by 5 get a gold background, checked *before* the even/odd check.
3. Wire the Contact Us form's Send button to `console.log` the typed values using `[(ngModel)]` (needs `FormsModule`).
