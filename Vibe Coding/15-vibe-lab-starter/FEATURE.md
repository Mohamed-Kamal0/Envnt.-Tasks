# Feature — Shopping cart

The one feature you'll run through today's two-session workflow. Don't start coding from this
file — start *planning* from it.

## Goal
Let a shopper add products to a cart and see what they've got, across the .NET API and the Angular
app.

## Scope (what "done" includes)
- An **"Add to cart"** button on each product card.
- A **cart count badge** in the header, showing how many items are in the cart.
- A **cart total** — what the cart is worth.
- Adding the same product twice should behave sensibly (increment a quantity, or add another
  line — your call, but **state which in the plan**).

## Constraints
- .NET 10 + Angular 18, **no new packages** on either side. Keep search and sort working.
- The cart lives behind the API, like every other piece of catalog state: a service behind an
  interface, a controller that stays thin, DTOs at the boundary.
- Angular keeps its shape: a service owns `HttpClient`, components render signals, the card stays
  presentational (`@Input()` in, `@Output()` out).

## Out of scope
Checkout, payments, users, persistence beyond in-memory, removing the search. Don't gold-plate.

## Decisions your plan must make explicitly
These are the ones an AI will happily make *for* you, badly, if the plan is vague:

1. **Where does the cart live** — server-side behind the API, or in Angular component state? (The
   constraint above answers this; your plan should say it out loud and say why it matters.)
2. **What does "add" send** — a product id, or an id and a price? Think about what happens if a
   browser is allowed to post its own price.
3. **Who computes the total** — the server or the browser? What goes wrong if both do?
4. **What happens when the id doesn't exist** — which status code, and what does the UI show?

## The workflow (this is the actual lesson)
1. **Session 1 — THINK.** In one AI session, produce a `plan.md` and **write no application code.**
   The plan names every file it will touch on both sides, the steps in order, and how each step is
   verified. Pressure-test it with [grill-me](tools/grill-me.md) before you close the session.
2. **Session 2 — BUILD.** In a *fresh* session, hand over `plan.md` and build by following it.
3. **Present** in the 4-part format:
   - what you asked for,
   - what the AI produced,
   - what you changed or rejected,
   - what you learned.
