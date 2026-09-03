# Lab 4 — Your AI toolbox (equip it once)

> **Run this against your OWN app.** Wherever a step names a bench file (`api/CatalogApi/Services/CatalogService.cs`, `web/src/app/product-list.ts`, …), use your own repo and your own file — this day's [bench](..) is the ready-made .NET + Angular scenario if your app won't start.

**Pillar:** Skills · **Time:** ~40 min · **Runs against:** your own app

Skills is the pillar about *equipping* the AI — tools it can call instead of things you paste by
hand. Today you add one for real, then package your first reusable prompt.

---

## Drill A — find and install a skill, from skills.sh (12 min)

[skills.sh](https://skills.sh) is a directory of rated Claude Code skills — ranked by install count
and source reputation, so you're not guessing which ones are worth trusting. Run
[`bash ../../tools/skills.sh`](../tools/skills.sh) first for a curated shortlist and a scan of
what you already have installed — it works even if the room's network is flaky.

1. Open skills.sh (or the curated list from `tools/skills.sh`) and find a skill useful to **your
   own app** — good starting points: **frontend-design** (a polished UI pass) or **pdf**
   (read/fill/build PDF files).
2. Install it: drop the skill folder into `~/.claude/skills/` (personal, works everywhere) or
   `.claude/skills/` inside your project (project-only) — or run `npx skills add <name>` to do it in
   one command.
3. Run `/reload-plugins` so your AI tool picks up the new skill.
4. Prove it two ways, against your own app:
   - **Auto-trigger:** ask for something the skill's description matches (e.g. "polish the styling on
     this page" for frontend-design). The AI should load the skill on its own — no invocation needed.
   - **On demand:** call it directly with `/<skill-name>` (e.g. `/frontend-design`) and confirm it runs.

**Notice:** you didn't teach the AI anything new — you handed it packaged know-how someone else
already tested. That's a Skill: capability you equip once, not knowledge you re-explain every
session.

> Didn't find one you like, or skills.sh is unreachable? You already have some — Claude Code ships
> with built-in skills for design, docs (PDF/Word), and research that auto-trigger with zero
> install. Try one of those instead, or run `tools/skills.sh` for the offline-friendly path.

> **Not a skill:** [grill-me](../tools/grill-me.md) is a *prompt template*, not something you
> install from skills.sh — you'll use it later today, in Session 1, to pressure-test your plan before you build.
> Worth a look now if you're curious.

> **Go deeper (optional, 5 min):** skills package know-how; MCP connectors give the AI *live tool
> access* instead — GitHub, a database, a browser. [Smithery](https://smithery.ai) is a free
> registry of MCP servers: install the GitHub MCP server, then ask "list my open PRs with one line
> each: title, age, what's blocking it." Notice the difference — a connector fetches live data, a
> skill packages a way of working.

## Drill B — turn a ticket into a playbook (8 min)

A **ticket** is a request with five parts: Goal (one sentence) · Context (files involved) ·
Constraints · Example of the expected result · Done when (observable). Package that shape once so
you never retype it:

**Claude Code** — create `.claude/commands/ticket.md` inside your project:

```markdown
Turn my request into a work ticket BEFORE doing anything:
- Goal (one sentence) · Context (files involved) · Constraints
- Example of the expected result · Output format · Done when (observable)
Show me the ticket, wait for my OK, then execute it.

My request: $ARGUMENTS
```

Now try it: `/ticket add a sort-by-price toggle`. It should draft the ticket, wait, then execute —
this is the feature you'll build in today's guided lab.

**Codex** — add the same instruction block to `AGENTS.md` under a heading like
`## When I say "ticket: <request>"`, then prompt `ticket: add a sort-by-price toggle`.

> **What you just built is a command, not a skill.** The difference matters, and it isn't pedantry:
> you type `/ticket` and it runs — deliberately, exactly when you ask. A skill that auto-drafted a
> ticket every time you described a feature would be exhausting, and it would kill the pause ("show
> me the ticket, wait for my OK") that the whole drill is built around.
>
> Three mechanisms, three folders. Know which is which before you reach for one:
>
> | | lives in | fires when | reach for it when |
> |---|---|---|---|
> | **Skill** | `.claude/skills/<name>/SKILL.md` | the model matches your request to its `description` | know-how should be applied *without being asked* |
> | **Command** | `.claude/commands/<name>.md` | you type `/<name>` | a ritual you want on demand, never by surprise |
> | **Agent** | `.claude/agents/<name>.md` | you hand it a job | work that needs its own context window and tools |
>
> You write an actual skill in Drill C, and an agent in today's task 3. Same week, three tools, and
> after today you'll know which one a problem is asking for.

## Drill C — author a real skill, with skill-creator (12 min)

Drill A installed someone else's skill. Drill B packaged a prompt *you* invoke. Now write a skill of
your own — know-how the AI applies **without being asked**.

The instructions are the easy half. The hard half is the `description:` line, because that line
**is** the trigger: too narrow and it never fires, too broad and it fires on everything. You cannot
tell which you wrote by reading it — you find out by testing it.

So don't hand-roll the file. Claude Code ships **skill-creator**, a skill whose entire job is
authoring skills — it scaffolds the folder, writes the frontmatter, and evaluates whether your
description actually triggers on the requests you meant:

```
/skill-creator
```

Or just say *"help me write a skill that captures how we build endpoints in this app"* and let it
auto-trigger. That's the demonstration, not a shortcut: a well-described skill firing on its own is
the exact behaviour you're trying to author.

**What to package.** Your app's conventions — the thing you re-explain in every session. On the
bench that's: *DTOs at the boundary, controllers stay thin, logic lives in the service, no `any` in
Angular state.* On your own app, whatever you keep correcting the AI about.

1. Run skill-creator and describe the know-how you want packaged.
2. Let it write `.claude/skills/<name>/SKILL.md`. **Read the `description:` it produced** — that is
   the whole trigger, and it is the one line worth arguing with.
3. `/reload-plugins`, then prove it fires **without** being named: ask for a new endpoint in plain
   language and watch whether the skill loads on its own.
4. If it doesn't fire, the description is wrong, not the AI. Sharpen it and try again.

> **Why this matters more than it looks.** Next week the final project runs for four days. A skill
> that carries your conventions is the difference between correcting the same three things every
> session and having them applied for free — and that is exactly what the ENVNT HelperSkills repo
> does at team scale: a single `intern-teaching-kit` skill packages how this entire programme gets
> built, decks and exercises alike. Ask your mentor to show you its `description:` line.

## Drill D — least privilege, on paper (5 min)

Before installing any skill or connector — the one you just added, or the next one — answer these
three (write them in your lab log):

1. What does this task actually **need** — read, or read *and* write?
2. What's the **worst thing** this skill or connector could do if the AI misunderstood you (or the
   skill itself was badly written)?
3. Could content it reads or fetches contain **instructions** (prompt injection)? What would you do
   about it?

If a skill or connector can write to something important, the answer usually includes: *keep a
human approval step in the loop.*

---

## Done when
- One skill from [skills.sh](../tools/skills.sh) (or a built-in skill) fires on your own app
  **both** ways — auto-trigger and `/<skill-name>`.
- `/ticket ...` (or your AGENTS.md equivalent) drafts a ticket and waits for your OK — and you can
  say why that one is a **command** and not a skill.
- A skill you wrote yourself lives in `.claude/skills/`, and it fired on a request that never named
  it.
- Your three least-privilege answers are in the lab log.

**Log line:** *"The skill I added today was ___, the one I wrote was ___, and the prompt I'll never
type again is ___."*
