# Student tasks — Week 3 · Day 14: The Model Brain

**Today's goal:** pick the right model for the job, then turn that judgment into a debugging habit —
read the error, hypothesize, ask one pointed question, escalate only if you're still stuck.
**You'll need:** an AI tool with a model or thinking-level switch (Claude Code's `/model`, Codex's
reasoning-effort setting, or OpenRouter), a git repo, and a bug.

**Which codebase?** [`starter/`](.) is today's bench: the .NET 10 API + Angular 18 Catalog
with **three planted defects**, one per layer. Tasks 4–6 debug those; tasks 1 and 7 run the same
method on **your own** app — once on a bug you plant yourself, once on one you find.

The method, every time — and it is the method, not the model, that's being graded:

1. **Read** the actual error or wrong behavior. API console, browser console, Network tab.
2. **Hypothesize** the cause in one sentence.
3. **Ask ONE pointed question** at the cheapest rung that could plausibly answer it — name the file
   and the symptom.
4. **Escalate one rung**, same prompt, only if that answer was wrong or shallow **twice**.
5. **Fix, confirm, and log which rung actually fixed it.**

## Before you start
- [ ] `cd starter/api/CatalogApi && dotnet run` — leave this terminal **visible**; two of today's
      symptoms print here, not in the browser.
- [ ] `cd starter/web && npm install && npm start` — open the browser console before you click
      anything.
- [ ] Both this folder and your own app are git repos with a clean commit, so every experiment is
      reversible.
- [ ] You can switch models or thinking depth in your AI tool.

## Tasks

### 1 · Read the error before you prompt it  ⏱ ~25
The whole day rests on this one reflex, so you practise it before the bugs get interesting. Break
something on purpose in your **own** app — rename a property the UI binds to, or return `null` where
a list is expected — run it, and stop at the error.

Do it in this order, out loud, before you type a single prompt:
1. **Read the error.** The whole message, including the stack trace's first frame in *your* code.
2. **Name the file and line.** Say them aloud. If you can't, you're not ready to prompt.
3. **Form a hypothesis.** One sentence: "X is null because Y never ran."
4. **Write ONE pointed prompt** at the *cheap* rung, naming that file and line — not "fix it".

- **Done when:** you have written down, in your lab log, the file:line and the one-sentence
  hypothesis **for a bug you diagnosed yourself**, plus the single prompt you sent. One prompt, not
  three. A cheap model fixed it, or you know precisely why it couldn't.
- The failure mode to catch yourself in: re-rolling a vague prompt because reading the stack trace
  felt slower. It never is.

### 2 · Write one agent — reasoning with its own context  ⏱ ~15
A third shape, and it belongs to Brain: a **skill** is instructions read inside your session, a
**command** is a ritual you invoke, an **agent** gets its own context window, its own tools, and its
own loop. That own window is the whole trade — the forty files it reads never land in your
conversation, and it cannot see what you just said.

Write one — `.claude/agents/reviewer.md`, frontmatter only: `name`, `description`, `tools`, then the
instruction. Point it at a diff you already understand.
- **Done when:** it comes back with findings you can check against your own reading of that diff, and
  you can say what it could **not** see — it never saw this conversation.
- Tie it back to today's pillar: an agent is a rung choice too. Give the cheap job its own cheap
  window instead of spending your expensive one on it.

### 3 · Brain-ladder drills  ⏱ ~10
[lab/01-brain-ladder.md](lab/01-brain-ladder.md) Drills A–C: the matching quiz, feel-the-difference
on a mechanical vs. a reasoning task, and the escalate-on-demand habit.
- **Done when:** quiz checked **with your mentor** (6/8 or better — they hold the key), and you
  saw with your own eyes that the cheap rung was just as correct on the mechanical task.

### 4 · The banner that isn't there  ⏱ ~15
Load the page. The "Featured today" banner reports a failure. Read the **API console** stack trace
before you read any C#.
- **Done when:** the banner shows a real product, and you can say in one sentence why the API
  answered 500 instead of returning nothing.
- Log: which rung fixed it?

### 5 · The filter that lies  ⏱ ~15
Click **"Under $50 only"**. Look at what comes back. No exception, no red text — just wrong.
- **Done when:** the button shows only products under $50, and you can name the single character
  that was wrong.
- Careful: fixing this in the Angular component by re-filtering the response leaves the API lying to
  every other client. The fix belongs where the bug is.
- Log: which rung fixed it?

### 6 · The defect that never throws  ⏱ ~20
Nothing in the browser or the console will point you at this one. Read the Angular app the way you'd
read a teammate's pull request — start at `web/src/environments/environment.ts` and follow where its
values go.
- **Done when:** the secret no longer ships to the browser, the Sync button still works, and you can
  explain where that value *should* live and what you'd do if it had already been pushed.
- Hint if you're stuck: everything in `environment.ts` is compiled into the bundle every visitor
  downloads.
- Log: reading found this, not running. Write that down.

### 7 · Individual task — fix a broken feature on your own app  ⏱ ~50
On **your own** app (or your mentor's planted bug), find something broken. Read the error before you
touch anything. Write one pointed prompt at the cheapest rung that could plausibly answer it.
Escalate only after **two** wrong or shallow answers in a row.
- **Done when:** the feature works, and you can say what the error meant *before* you asked the AI —
  plus which rung actually fixed it, logged.
- Stuck? Re-read the error message itself before escalating — the fix is usually named in the text.

### 8 · Right-size on purpose  ⏱ ~10 (in the wrap-up)
Look back at your log. For every bug you touched today — the one you planted in task 1, the bench's
three, and the one on your own app — was the rung you used the cheapest one that could have worked?
- **Done when:** you can state your new default rung and the condition under which you escalate.

## Verify

```bash
cd starter/api/CatalogApi
dotnet run &
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5144/api/products/featured   # 200, not 500
curl -s "http://localhost:5144/api/products?cheapOnly=true"                            # only prices < 50
grep -ri "sk_live" ../../web/src || echo "no secrets in client source"                 # must print the fallback
```

```bash
cd starter/web
npm run build            # compiles clean
```

## End-of-day deliverables
- [ ] One bug you planted yourself diagnosed to file:line with a written hypothesis, fixed with ONE
      pointed prompt at the cheap rung
- [ ] One agent written and run, and you can say what it could not see
- [ ] Brain-ladder quiz done, checked with your mentor (6/8 or better)
- [ ] All three starter defects fixed via read → hypothesize → ask → escalate, fixing rung logged for each
- [ ] One bug on your own app fixed the same way, escalated only after two wrong answers
- [ ] Can state the cost equation and explain why right-sizing is the biggest lever

## Finished early?
- Ask your mentor for the .NET minimal-API variant of the brain-ladder drills for
  the same exercise at the smallest possible scale.
- Look ahead: tomorrow (Day 15) you stop repeating
  yourself: the rules you have been typing all week become a skill on disk, and then you ship a
  feature end to end.

---

`solution/` is the fixed bench, for **after** an honest attempt.
