# Vibe Coding — helper tools

Every helper tool named across Week 3 (Days 12–15), in one place. All of them are real, free (or
free-tier), and tool-agnostic — nothing here requires a paid seat.

| Tool | Pillar / Day | What it is | Where |
|---|---|---|---|
| **skills.sh** | Skills · Day 15 | The real Claude Code skill mechanism (`SKILL.md`, `~/.claude/skills/`, `.claude/skills/`) plus the community skill registry. | [run locally](skills.sh) (`bash tools/skills.sh`) · [browse online](https://skills.sh) |
| **grill-me** | Ship · Day 15 | A prompt template you paste into any AI to pressure-test a plan before you build it — assumptions, acceptance criteria, security, edge cases, over-scope. | [grill-me.md](grill-me.md) |
| **OpenRouter** | Brain · Day 14 | A model router / API aggregator — one API key, many models, pay-per-token pricing. Used to feel cost = tokens × price directly. | [openrouter.ai](https://openrouter.ai) |
| **LlamaIndex** | Context · Day 13 | Indexes a codebase once (split → embed → store), then answers each question with only the handful of chunks that relate to it. Retrieval is "point, don't dump" done by machine. | `pip install llama-index` · [llamaindex.ai](https://www.llamaindex.ai) |
| **CLAUDE.md / AGENTS.md** | Context · Day 13 | Not a download — a plain-text memory file at your project root with checkable conventions, read automatically every session. | Start one in your own app's root |

## Why this folder exists

Earlier drafts of this program referenced a couple of tools by name without anything backing the
name up in the repo — a student clicking the link found nothing. Two fixes were needed:

- **skills.sh** and **grill-me** are now real, runnable/usable files in this folder (see above) —
  no external account or install required to start.
- **OpenRouter**, **LlamaIndex**, and memory files (`CLAUDE.md`/`AGENTS.md`) are all real; they're
  listed here too so this page is the single place to look up any of Week 3's helper tools.

**Not here:** a tool once called "Jumbo" that never existed anywhere except in prose.

## Why LlamaIndex and not a repo-packer

Day 14's whole argument is **point, don't dump**. The Context slot used to name **Repomix**, which
packs an entire repo into one file you paste — a tool whose pitch is the exact habit the day tells
interns to break. Retrieval is the aligned answer: index once, then pull back the few chunks a
question actually needs. Same idea as naming two files by hand, done automatically and at a scale
hand-picking can't reach.

**LangChain + LangGraph** are named on the same slide's GO DEEPER line, deliberately as a *pointer,
not a lesson*: they orchestrate multi-step agents — chains, state, retries — which is a different
job from feeding one session the right page. Interns curious about where this goes next should read
them after the week, not during it.

Neither is a required install: both are Python-side, and every Day 13 deliverable can be met with
named files and a `CLAUDE.md`. LlamaIndex is there for the intern who asks "what happens when the
repo is too big to point at?" — which is the right question, and has a real answer.

## Bring your own

Every lab in this week is tool-agnostic — swap in whatever AI coding tool you're using (Claude
Code, Codex, Gemini CLI, …) and whatever skill/context tooling it supports. The specific tools
above are the ones this program demos and has verified; they are starting points, not requirements.
