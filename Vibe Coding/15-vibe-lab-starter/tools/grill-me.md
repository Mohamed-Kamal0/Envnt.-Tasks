# grill-me — pressure-test a plan before you build it

**What this is:** a prompt template, not an installable skill. Paste the block below into any AI
chat (Claude, Codex, Gemini, whatever you're using) along with your ticket or `plan.md`, before you
let it write a single line of code. It makes the AI argue with your plan instead of agreeing with
it — catching the gaps you'd otherwise only find in review, or in production.

**Used on:** Day 15 (Skills & Ship) — grill your ticket or `plan.md` at the end of Session 1
(THINK), before Session 2 (BUILD) starts. Nothing stops you from running it earlier — a rough idea
on Day 12 benefits just as much as a polished `plan.md` on Day 15.

---

## The prompt

Copy everything between the lines and paste it into your AI, followed by your plan or ticket.

```
You are reviewing my plan before I let you (or anyone) build it. Do not be encouraging.
Do not soften anything to spare my feelings. Your job is to find the reasons this plan
fails, not to cheer it on.

Go through EACH of these five lenses, one at a time. For each one, either name a real
problem with MY specific plan, or say "no issue found" — never pad with generic advice.

1. ASSUMPTIONS
   What am I assuming that isn't actually written down or verified? (a library exists,
   an API returns what I think, a file is where I think it is, a user behaves a certain
   way)

2. ACCEPTANCE CRITERIA
   Is "done" observable, or is it a feeling? For each requirement, could a reviewer who
   didn't write this plan check it against the running app in under 30 seconds? Flag any
   "done when" that's vague ("works well," "handles errors," "is fast").

3. SECURITY
   Where does user input reach a database, a shell command, a file path, or another
   service? Is there anything here that touches secrets, auth, or permissions? What's the
   worst thing a malicious input could do at each of those points?

4. EDGE CASES
   What are the 3 inputs most likely to break this — the empty case, the huge case, and
   the malformed case? What happens on network failure, or if the AI's own code throws
   halfway through?

5. OVER-SCOPE
   What's in this plan that I didn't actually need? What's the smallest version that
   still satisfies the real goal? Am I about to touch files or add dependencies that
   aren't required?

Ask me ONE clarifying question at a time for the biggest gap you found — wait for my
answer before asking the next one. Stop and summarize once the plan is solid enough to
build, or after 5 questions, whichever comes first.

Here is my plan:
[paste your ticket or plan.md here]
```

## Why one question at a time

An AI that dumps all five lenses' worth of objections at once is easy to skim past. Answering one
question, then getting the next, forces you to actually think about each gap instead of
rubber-stamping a wall of text. If an answer reveals a real hole, fix the plan before moving on —
don't just note it and keep going.

## Done when

- You ran the five lenses against your real plan or ticket (not a toy example).
- At least one answer changed something in your plan — a `Done when` line got sharper, a security
  check got added, scope got cut. If nothing changed, you probably pasted a plan that was already
  too vague to grill — tighten it and try again.
- The revised plan is what you hand to Session 2 (BUILD).

**Log line:** *"grill-me caught ___ before I built it, which would have cost me ___ to find later."*
