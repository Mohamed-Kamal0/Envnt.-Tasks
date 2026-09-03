# Student tasks — Week 3 · Day 13: Context — Feed It the Right Page

**Today's goal:** show the AI less, on purpose, and get better results — then prove it with a memory
file and a refactor you didn't have to eyeball to trust. **You'll need:** a git repo and an AI tool.

**Which codebase?** Your own Week 1–2 app wherever a task says "your own". [`starter/`](.) is
the worked bench: the .NET 10 API + Angular 18 Catalog, plus an xUnit project that is green and
nearly empty — today you fill it.

## Before you start
- [ ] `cd starter/api && dotnet test` → **1 passing** test (the smoke test). If that's red, fix it
      before anything else.
- [ ] `cd starter/web && npm install && npm start` → the grid renders.
- [ ] Your own app runs and is committed clean.
- [ ] You know where your loading/fetch state lives (you'll plant a bug there on purpose).

## Tasks

### 1 · Fill the window on purpose — and watch the middle drop  ⏱ ~25
Everything today rests on one claim from the theory block: the window is finite, and a model reads
the **start and end** of a long prompt well while it **skims the middle**. Don't take that on trust —
make it happen to you.

Build one deliberately bloated prompt against your own app. Paste a long file (200+ lines; concatenate
two if you have to), and plant **three independently checkable instructions**:

- at the very **top**: *"Answer as a numbered list."*
- buried in the **middle of the pasted code**: *"Prefix every function name you mention with 🔧."*
- at the very **end**: *"Finish your reply with the word DONE."*

Run it once. Score all three: landed, or ignored?

- **Done when:** you can name which position got dropped — then you moved that same instruction to the
  end, re-ran, and it landed. Same words, same model, different position, different outcome.
- **All three landed?** Your prompt wasn't long enough. Paste more until one drops. Finding the edge
  *is* the exercise — you're measuring your tool, not failing the drill.
- If your tool shows a context or token count, note it before and after that paste — that number is
  the budget every later task spends. Copilot doesn't show one; skip it and judge by the drop-out
  instead, which is the part that matters anyway.

### 2 · Point, don't dump — feel the difference  ⏱ ~15
[lab/01-context-lab.md](lab/01-context-lab.md) Drill A, on your own app: plant a real bug on purpose
(flip `loading.set(false)` to `true` in the success path), run the vague prompt, then the pointed one.
- **Done when:** the weak run visibly wandered — more files opened, more guessing, or just longer —
  than the run that named the file and the symptom.

### 3 · Write the conventions down once  ⏱ ~10
Start a memory file at the root of the bench **and** one in your own app's root — `CLAUDE.md` for
Claude Code, `AGENTS.md` for Codex, `.github/copilot-instructions.md` for Copilot in VS Code.
4–6 **checkable** conventions covering both halves: where pure logic lives, what stays out of the
controller, what the Angular service owns, whether new packages are allowed.
- **Done when:** every line could be answered yes/no by someone reading a diff. "Write good code" is
  not checkable. "No new NuGet or npm packages" is.

### 4 · The guarded refactor — test first  ⏱ ~25
Follow [`starter/REFACTOR.md`](REFACTOR.md): extract the inline search filter out of
`CatalogService.GetProductsAsync` into a pure `ProductFilter`, covered by xUnit tests written
**before** the code moves. Ask for the tests, read them, *then* give the go-ahead for the extraction.
- **Done when:** `dotnet test` is green, the tests describe the *existing* behavior (including the
  case-sensitivity), and `CatalogService` calls the extracted function.
- **The trap — this is the actual exercise:** the AI will want to "improve" the case-sensitive match
  to `OrdinalIgnoreCase` while it's in there. That's a behavior change wearing a refactor's clothes,
  and it passes a casual review because it looks like a fix. Catch it in the diff. A refactor that
  changes behavior isn't a refactor — it's an unreviewed feature.

### 5 · Did the memory file do any work?  ⏱ ~10
Ask for something small — say a `GET /api/products/categories` endpoint — **without** repeating any
convention from your `CLAUDE.md` in the prompt.
- **Done when:** you can point at the result and say which of your written conventions it followed
  unprompted, and which it ignored. The ignored one needs rewording, not repeating.
- **Then verify what came back** — three checks, every time, from today's theory:
  1. Did it change **only** the files you named, or did it wander into others?
  2. Did it follow the memory file even though you never repeated it?
  3. Did it use information you never gave it? That's a mistake, not a good sign.
  Reading the output is context work too. Your job isn't done at "send".

### 6 · Search over paste  ⏱ ~10
Ask: *"Search this project for every place a product's price is read or formatted — C# and TypeScript
both — and list file + line + what it's used for. Don't show me the file contents, just the map."*
- **Done when:** you have the map without pasting a single file into the prompt.

### 7 · Individual task — one guarded refactor on your own app  ⏱ ~45
Now do task 3's shape on **your own** code: pick one small piece worth cleaning up, ask for the test
first, get your OK, let the AI refactor, then confirm the test still passes.
- **Done when:** the refactor changed structure but not behavior, and a test — not a glance — proves
  it.

## Verify

```bash
cd starter/api
dotnet test              # green: the smoke test + your 4 filter tests
```

```bash
cd starter/api/CatalogApi
dotnet run &
curl "http://localhost:5144/api/products?search=Mouse"   # 1 product
curl "http://localhost:5144/api/products?search=mouse"   # [] — case-sensitive, unchanged on purpose
```

## End-of-day deliverables
- [ ] Lost-in-the-middle reproduced on your own prompt — you can name the position that got dropped,
      and the same instruction landed once moved to the end
- [ ] Context-lab bug fixed via a pointed prompt, after seeing the vague one wander
- [ ] A memory file written on your own app — whichever name your tool reads — with checkable conventions — and respected
      without repeating it in the prompt
- [ ] One guarded refactor completed, proven by a test you ran yourself
- [ ] Can state the mantra — curate · cache · remember — with one example of each

## Finished early?
- Find a second refactor candidate in your own app and run the same guarded loop.
- Look ahead: tomorrow (Day 14) is about which model
  to hand the job to. Today you learned to send the right context; tomorrow you learn that a bigger
  brain does not rescue a bad one.

---

`solution/` holds the reference extraction, its four tests, and a worked `CLAUDE.md` — for **after**
you've done it yourself.
