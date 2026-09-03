# Lab 3 — Brain ladder (right-size the model)

> **Run this against your OWN app.** Wherever a step names a bench file (`api/CatalogApi/Services/CatalogService.cs`, `web/src/app/product-list.ts`, …), use your own repo and your own file — this day's [bench](..) is the ready-made .NET + Angular scenario if your app won't start.

**Pillar:** Brain · **Time:** ~35 min

Don't use the biggest brain for everything. This lab trains the *instinct* of picking the rung.

---

## Drill A — the matching quiz (5 min)

For each task, pick a rung: **Deep** (hard reasoning) · **Balanced** (daily driver) · **Fast**
(cheap & instant). Write your answers down, then check them with your mentor — they hold the key.

| # | Task | Your rung |
|---|------|-----------|
| 1 | Rename `products` to `items` across the project | |
| 2 | Design the folder structure for a new 10-screen app | |
| 3 | Add a prop to a component and update its 3 call sites | |
| 4 | Debug a race condition that only happens on slow networks | |
| 5 | Write 20 commit messages from 20 small diffs | |
| 6 | Choose between REST and WebSockets for a live dashboard | |
| 7 | Convert 15 CSS files to the team's naming convention | |
| 8 | Explain why an Angular signal update doesn't re-render until change detection runs | |

## Drill B — feel the difference (8 min)

Run the **same two tasks** at two different settings and compare quality, speed, and cost-feel.

- *Claude Code:* switch with `/model` (e.g., Haiku ↔ Opus), or keep the model and change the
  **thinking level**. *Codex:* change the reasoning-effort setting.

**Task 1 — mechanical** (any rung can do it):

```
In your own app, rename the CSS class "card" to "product-card" everywhere it appears.
```

**Task 2 — reasoning** (rungs will differ):

```
In an Angular service, a search box calls HttpClient.get() on every keystroke and stores the
response in a signal. On a slow network the results sometimes show the SECOND-to-last query's
products instead of the last one. Explain exactly why, and show the RxJS operator that fixes it.
```

**Observe:** on Task 1, the cheap setting was just as correct — the extra power bought nothing.
On Task 2, compare the *depth* of the explanation. That asymmetry is the whole lesson.

## Drill C — escalate on demand (2 min, a habit not a task)

Adopt this default: **start one rung lower than you think you need.** If the answer is shallow or
wrong, escalate one rung *with the same prompt*. You'll be surprised how often the lower rung was
enough — and escalating costs one retry, while over-provisioning costs on every call.

---

## Drill D — debug the starter, by escalation (20 min)

This is the day's guided lab. This day's [bench](..) has **three planted defects**, one
per layer — a 500 from the API, a filter that does the opposite of its label, and a hardcoded
secret in the Angular client. Fix them with the method, not with re-rolling (full task sheet:
[the task sheet](../TASKS.md)):

1. **Read** the actual error or wrong behavior — console message, wrong products showing, whatever
   it is. Don't guess before you've read it.
2. **Hypothesize** out loud (or in your lab log) what's causing it, in one sentence.
3. **Ask ONE pointed question**, at the cheapest rung that could plausibly answer it — name the
   file and the symptom, e.g.:
   ```
   In api/CatalogApi/Services/CatalogService.cs, the "Under $50 only" button shows products
   OVER $50 instead of under. Look at the cheapOnly branch of GetProductsAsync.
   What's the one-line fix?
   ```
4. **Escalate one rung**, same prompt, only if that answer is wrong or shallow **twice in a row**.
   Most of these three bugs don't need it — that's the point.
5. **Fix, confirm, log which rung actually fixed it** before moving to the next defect.

The third defect (a hardcoded-looking secret) won't throw an error — you'll only catch it by
*reading* `web/src/environments/environment.ts` and following where its values go, like a reviewer
would. That's deliberate: not every bug announces itself.

> Stuck for more than two escalations on the same defect? That's the sign to stop and change the
> prompt, not the rung — re-read Drill C's rule.

---

## Done when
- Quiz answers checked with your mentor (6/8 or better).
- You saw with your own eyes: cheap = same result on mechanical work, deep = better on reasoning.
- All three starter defects fixed, each via read → hypothesize → ask → escalate-if-needed, with the
  fixing rung logged for each.

**Log line:** *"My default rung is now ___, and I escalate when ___."*
