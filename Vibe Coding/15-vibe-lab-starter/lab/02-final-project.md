# Lab 5 — Final project: one feature, all four pillars

> **Run this against your OWN app.** Wherever a step names a bench file (`api/CatalogApi/…`, `web/src/app/…`), use your own repo and your own file — this day's [bench](..) is the ready-made .NET + Angular scenario if your app won't start.

**Pillars:** all · **Time:** ~30 min · **Runs against:** your own app

One real feature, built the vibe-coding-done-right way. You'll use everything from the week's four
pillar days **plus** the workflow from today's slides: **plan in one session, build in a fresh one.**

---

## The feature

> **Add a "max price" filter to the product list:** a `maxPrice` query parameter on
> `GET /api/products`, and a small input above the grid that sends it; only products at or under
> that price come back; clearing it shows everything; the loading/error states stay intact.

## Before you start — the safety net (1 min)

```bash
git add -A && git commit -m "before final project"
```

Git is your undo button. Every AI experiment is now reversible. (No git repo yet? `git init` first.)

## Session 1 — THINK (~10 min): plan it, don't build it

Open a session whose only job is producing a **written plan**. No code in this session.

**1 · Skills — use your playbook.** Start with the `/ticket` command you built an hour ago in
[Drill B](01-skills-toolbox.md):
`/ticket add a max-price filter …` (or your `AGENTS.md` trigger). Let it draft the ticket.

**2 · Brain — pick the rung, out loud.** Is this Deep, Balanced, or Fast work? Write your choice +
a one-line reason in the lab log. (A small, well-scoped UI feature on a known codebase… you know
the answer.)

**3 · Context — set the desk.** In the ticket's Context line, point at the exact files on both
sides (`api/CatalogApi/Controllers/ProductsController.cs`,
`api/CatalogApi/Services/CatalogService.cs`, `web/src/app/product.service.ts`,
`web/src/app/product-list.ts`) — no dumping, no vagueness. Your `CLAUDE.md`/`AGENTS.md` from Day 13
handles the conventions.

**4 · Ask for options first.** Before approving the plan, ask:

```
Give me 2 ways to implement this filter (e.g., filter server-side in CatalogService vs. filter
the fetched array in the Angular component), with one pro and one con each. Recommend one.
```

**5 · Write the plan down.** End the session by asking:

```
Write the agreed plan to plan.md: the ticket (Goal / Context / Constraints / Example /
Output / Done when), the chosen approach and why, and the implementation steps in order.
```

The ticket must include: *Example* ("`?maxPrice=40` → only Wireless Mouse, USB-C Hub, Laptop Stand
and Desk Mat come back") and *Done when* (the checklist below). **Close the session.** `plan.md` is
the hand-off.

## Session 2 — BUILD (~15 min): fresh session, execute the plan

Open a **new session** (empty context — this is the point). Give it one thing:

```
Read plan.md and implement it, step by step. After each step, tell me what changed
and how to verify it before moving to the next.
```

- **Small steps:** let it finish a step, verify, then continue — never 200 lines at once.
- **Let it check its own work:** give it the run commands (`dotnet run` in `api/CatalogApi`,
  `npm start` in `web`, plus a `curl` for the endpoint) so it can confirm.
- **Stuck twice? Start over:** if the session goes in circles two times, don't keep fighting —
  `git checkout .`, improve `plan.md` with what you learned, and open another fresh session.

## Done when (all four, observed with your own eyes)

- [ ] Entering `50` hides products above $50; clearing shows all 8 again
- [ ] `curl "http://localhost:5144/api/products?maxPrice=40"` returns the same set the UI shows
- [ ] "Loading products…" still appears briefly on refresh
- [ ] No new NuGet or npm packages; conventions from your memory file respected
- [ ] **You read the whole diff** and can explain every changed line to a mentor

## The review (don't skip — this is the craft)

1. Read the diff line by line: `git diff`. Anything you can't explain → ask the AI to explain it,
   then decide.
2. Run both halves; try the edge cases: `0`, an empty box, a huge number, a negative number,
   deleting the text, and `?maxPrice=abc` straight at the API.
3. One improvement pass, told as a constraint, not a re-roll:
   ```
   Good. Now debounce the filter input by 200ms using RxJS, without adding a package.
   ```
4. Happy? Commit: `git add -A && git commit -m "max-price filter"`.

## Debrief (5 min, with your mentor or buddy)

- Which pillar did the most work for you? Which did you almost skip?
- What did the fresh BUILD session do better than a long, crowded one would have?
- Show your lab log's four "log lines" — that's your personal vibe-coding playbook, v1.
- Fill the **After** column below. If you rated yourself on Day 12 you have a Before; if not,
  rate both now and be honest about where you started.

  | Pillar | Before | After |
  |--------|--------|-------|
  | Prompt — I write tickets, not wishes | | |
  | Context — I point, cache, and persist | | |
  | Brain — I pick the rung deliberately | | |
  | Skills — I package playbooks & use connectors | | |

**You're done when:** the feature works, the diff is fully understood, and your scorecard moved.
