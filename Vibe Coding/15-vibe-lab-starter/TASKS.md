# Student tasks — Week 3 · Day 15: Skills & Ship

**Today's goal:** build one real feature end to end — plan it in one session, build it in a fresh
one — then present it like a professional. **You'll need:** a clean, committed repo, your AI tool,
and a feature small enough to finish today.

**Which codebase?** Your own Week 1–2 app, first choice — today is the dress rehearsal for the final
project. No feature in mind, or your app won't start? Use [`starter/`](.) +
[`starter/FEATURE.md`](FEATURE.md): a shopping cart that has to cross both halves of the
stack, which is exactly what makes the planning matter.

## Before you start
- [ ] Both halves run: `dotnet run` on 5144, `npm start` on 4200 (or your own app's equivalent).
- [ ] `git status` shows nothing pending.
- [ ] You've picked **one** small feature — ask your mentor to size-check it if you're unsure.
- [ ] A network that can reach [skills.sh](https://skills.sh) — if it's blocked, run
      `bash tools/skills.sh` instead: a curated shortlist plus a scan of what you already have,
      fully offline. It ships in this folder.

## Tasks

### 1 · Equip your toolbox — install a skill  ⏱ ~25
Follow [lab/01-skills-toolbox.md](lab/01-skills-toolbox.md) drills A and B: find and install one skill from
[skills.sh](https://skills.sh) (frontend-design and pdf are safe picks), then package the ticket
format into a reusable `/ticket` command. The five rows — Goal / Context / Constraints / Example /
Done when — are revised at the end of this morning’s deck, before you
get to Session 1; the command just saves you retyping them.
- **Done when:** the skill fires **both** ways — auto-triggered by a matching request, and called
  directly as `/<skill-name>` — and `/ticket …` drafts a ticket and waits for your OK.
- **Done when, part two:** you can say why `/ticket` is a **command** and not a skill. A command runs
  when you type it; a skill fires when the model decides your request matches its description. The
  ticket ritual wants the first — you want that pause exactly when you ask for it, never guessed at.
- **Pick one you will actually use on the final project.** That starts on Monday, and this is the
  last taught day before it — a skill installed for a bench you abandon on Friday is a demo, not a tool.

### 2 · Write a skill of your own — with skill-creator  ⏱ ~20
Installing someone else's skill is not the same as writing one. Follow
[lab/01-skills-toolbox.md](lab/01-skills-toolbox.md) drill C: package the conventions you keep
re-explaining to the AI about **your own app** — DTOs at the boundary, thin controllers, no `any` in
Angular state, whatever you keep correcting.

Use **skill-creator** (`/skill-creator`, or just describe what you want and let it auto-trigger — a
skill firing on its own is the behaviour you're trying to author). It writes
`.claude/skills/<name>/SKILL.md` and, more importantly, tests whether the `description:` actually
triggers on the requests you meant.
- **Done when:** your skill fires on a request that **never names it** — you asked for an endpoint in
  plain language and it loaded itself.
- If it doesn't fire, the `description:` is wrong, not the AI. That one line is the entire trigger:
  too narrow and it never fires, too broad and it fires on everything. Sharpen it and try again.
- **This is the one that pays next week.** Four days of final project, and a skill carrying your
  conventions is the difference between correcting the same three things every session and having
  them applied for free.

### 3 · The safety-net commit  ⏱ ~1
`git add -A && git commit -m "before full AI day"`.
- **Done when:** `git log` shows it — your undo button for the whole day.

### 4 · Session 1 — THINK  ⏱ ~60
One session, **no application code**. Ticket it (Goal / Context / Constraints / Example / Done when),
say the brain rung out loud, point Context at the exact files, ask for **2** implementation options,
then write the agreed plan to `plan.md`. Before closing, grill it: paste `plan.md` into the
[grill-me](tools/grill-me.md) template and fix whatever it surfaces.

If you're building the cart, the plan must answer these explicitly — an AI will happily decide them
for you, badly, if the plan is vague:
- Does the cart live **server-side** behind the API, or in Angular component state?
- What does "add" send — a product id, or an id *and* a price?
- Who computes the total, and what goes wrong if both sides do?
- What happens when the id doesn't exist — which status code, and what does the UI show?

- **Done when:** `plan.md` names every file it will touch on both sides, it survived a grill-me pass,
  and the session ended with zero application code written.
- Stuck? If you're tempted to start coding in this session, that's the sign to close it.

### 5 · Session 2 — BUILD  ⏱ ~50
Open a **genuinely fresh** session (empty context). Tell it to read `plan.md` and implement it step
by step, verifying after each step.
- **Done when:** the feature works exactly as `plan.md`'s Done-when describes. For the cart: the
  badge counts, the total adds up, adding the same product twice increments a quantity, and search
  still works.
- Stuck twice on the same thing? `git checkout .`, improve `plan.md` with what you learned, and open
  another fresh session.

### 6 · The review  ⏱ ~20 (part of Session 2's close)
Read the whole diff line by line (`git diff`), the way a reviewer would:
- Does the **server** own the price, or did the client get to post one? (Posting a price is the bug
  that lets a shopper pay a cent for a $429 monitor — and it passes a casual review because the
  feature "works".)
- Does the card stay presentational, or did a service get injected into it?
- Are the DTOs at the boundary, or did an entity leak out?

Test the edge cases, then ask for one improvement **as a constraint, not a re-roll**. Commit when
happy.
- **Done when:** you can explain every changed line to a mentor, unaided.

### 7 · Presentation prep  ⏱ ~10
Prepare a 2-minute walkthrough: **1 Prompt** (your ticket) · **2 AI Output** (what it produced) ·
**3 Review Notes** (what you changed or questioned after reading the diff) · **4 Final Changes**
(the committed result).
- **Done when:** you can deliver it without reading from the screen.

## Verify

```bash
cd starter/api/CatalogApi
dotnet run &
curl -s -X POST http://localhost:5144/api/cart/items \
  -H "Content-Type: application/json" -d '{"productId":1}'          # 200 + the updated cart
curl -s -X POST http://localhost:5144/api/cart/items \
  -H "Content-Type: application/json" -d '{"productId":1}'          # same line, qty now 2
curl -s -o /dev/null -w "%{http_code}\n" -X POST http://localhost:5144/api/cart/items \
  -H "Content-Type: application/json" -d '{"productId":999}'        # 404, not a phantom line
curl -s http://localhost:5144/api/cart                              # itemCount + total add up
```

```bash
cd starter/web
npm run build             # compiles clean
```

## End-of-day deliverables
- [ ] One skill installed and proven both ways — auto-trigger and `/<skill-name>`
- [ ] `/ticket` works, and you can say why it's a command rather than a skill
- [ ] One skill **you wrote**, in `.claude/skills/`, that fired on a request which never named it
- [ ] Safety-net commit exists before Session 2 started
- [ ] `plan.md` written in Session 1; Session 2 was a genuinely fresh window
- [ ] Feature works per `plan.md`'s Done-when
- [ ] Every changed line explained to a mentor, unaided — the JUDGING rule (you get the full rubric on Day 16)
- [ ] 4-part presentation ready: Prompt · AI Output · Review Notes · Final Changes

## Finished early?
- Add a second small feature the same way and see how much faster the second pass goes now that
  `plan.md` is a habit.
- Look ahead: day 16 scales this exact workflow to a whole project — same two sessions, four days.

---

`solution/` is one correct shape — read it **after** your own build. A different-but-correct cart is
a good outcome; matching it line for line is not the goal.
