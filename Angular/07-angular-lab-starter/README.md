> 🚧 **This is the STUDENT STARTER version** — the markup below is intentionally incomplete. Follow the `#Task` comments in each component's `.html` file. See `ecommerce-startup/day-01` for the completed reference solution if you get stuck.

> Styling (CSS) is provided for you in every component — your hands-on work is the component structure/template only.

Every component/page's `.ts` and `.html` file currently has minimal working scaffolding so the project compiles for testing. Before handing this to students, delete the `.ts` and `.html` files listed below (keep the `.css` files — they're already fully styled and are not part of the exercise). Once deleted, `ng serve` will show "cannot find module" errors until each one is rebuilt — that's the intended signal. See `src/app/app.component.ts` for the overview of what each one should do and how they fit together.

## 🎯 Your Tasks

- **#Task 1** — Create and implement the Navbar component. Path: `src/app/components/navbar/navbar.component.ts` + `.html` (scaffold with `ng generate component components/navbar --standalone`, or create the files manually — the matching `.css` file is already provided, don't recreate it). Build a top navigation bar with a brand link on the left reading "ShopEase" and, on the right, four plain placeholder links for Home, Shop, About Us, and Contact Us.
- **#Task 2** — Create and implement the Footer component. Path: `src/app/components/footer/footer.component.ts` + `.html` (scaffold with `ng generate component components/footer --standalone`, or create the files manually — the matching `.css` file is already provided). Build a footer with muted credit text ("ShopEase · built in this Angular course") on the left and a dynamic copyright year on the right.
- **#Task 3** — Create and implement the Hero component. Path: `src/app/components/hero/hero.component.ts` + `.html` (scaffold with `ng generate component components/hero --standalone`, or create the files manually). Build a hero banner with a "New season" badge, a headline reading "Everything you need, one cart away.", a short descriptive paragraph, and a "Shop now" call-to-action link.
- **#Task 4** — Create and implement the Home page component. Path: `src/app/pages/home/home.component.ts` + `.html` (scaffold with `ng generate component pages/home --standalone`, or create the files manually). Compose the Navbar, Hero, Products, and Footer components together, in that order, to form the landing page.
- **#Task 5** — Create and implement the About Us page component. Path: `src/app/pages/about-us/about-us.component.ts` + `.html` (scaffold with `ng generate component pages/about-us --standalone`, or create the files manually). Build a simple page section with an "About Us" heading and an introductory paragraph about ShopEase.
- **#Task 6** — Create and implement the Shop page component. Path: `src/app/pages/shop/shop.component.ts` + `.html` (scaffold with `ng generate component pages/shop --standalone`, or create the files manually). Build a page section with a "Shop" heading and reuse the existing Products component beneath it.
- **#Task 7** — Create and implement the Contact Us page component. Path: `src/app/pages/contact-us/contact-us.component.ts` + `.html` (scaffold with `ng generate component pages/contact-us --standalone`, or create the files manually). Build a page section with a "Contact Us" heading and a compact form containing an email field, a multi-line message field, and a "Send" button that isn't wired up yet.

# Day 01 — Components & Pages

## Hands-on

Generate components: `NavbarComponent`, `FooterComponent`, `HeroComponent`, `ProductsComponent`.
Generate pages: `Home`, `About Us`, `Shop`, `Contact Us`.

## Run it

```bash
npm install
npm start
```

## What was built

```
src/app/
  components/
    navbar/      static links for now (href="#")
    footer/      copyright bar
    hero/        landing banner
    products/    empty placeholder — gets data in Day 2
  pages/
    home/        composes navbar + hero + products + footer
    about-us/    placeholder
    shop/        reuses <app-products>
    contact-us/  a plain contact form (not wired up yet)
  app.component.ts   shows <app-home /> directly — no router yet
```

## Why no routing yet

`ng generate` gives you a component, not a place in the URL. Before Day 2 there is
no `app.routes.ts`, so `About Us`, `Shop` and `Contact Us` exist as files but are
not reachable from the browser — only `Home` is shown, hard-coded in
`app.component.ts`. That is deliberate: it isolates "how do I build a component"
from "how do I navigate between components", which is Day 2's topic.

## Try it yourself

1. Add a fifth component, `TestimonialComponent`, and drop it into `HomeComponent`
   under `<app-products />`.
2. Give `HeroComponent` an `@Input() headline` so `HomeComponent` can customise the
   banner text.
