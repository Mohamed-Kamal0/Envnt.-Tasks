# ENVNT Tasks

Hands-on software engineering labs for the ENVNT intern program. The repository progresses from
individual .NET and Angular exercises to integrated, AI-assisted full-stack workflows.

## Repository layout

### [.NET labs](Dotnet/)

Six standalone labs (`01`-`06`) covering C# and ASP.NET Core fundamentals. The later labs include
catalog APIs, console applications, and xUnit test projects. The projects target **.NET 10**.

### [Angular labs](Angular/)

Five standalone labs (`07`-`11`) covering Angular application development. The projects use
**Angular 18** with standalone components, TypeScript, services, signals, and HTTP calls.

### [Vibe Coding labs](<Vibe Coding/>)

Four integrated full-stack labs (`12`-`15`) using a .NET catalog API and an Angular client:

- **Day 12 — Meet Your AI Pair:** implement a small feature across the API and UI.
- **Day 13 — Context:** write project conventions and perform a test-first refactor.
- **Day 14 — The Model Brain:** debug defects one layer at a time using the right level of tooling.
- **Day 15 — Skills & Ship:** plan a feature in one session, build it in a fresh session, review the
  diff, and present the result.

Each lab folder contains its own README and task-specific instructions. The Day 15 starter also
includes a catalog shopping-cart feature brief, skills examples, helper tools, and final-project
guidance.

## Typical full-stack setup

The integrated starters are organized as:

```text
<day>-vibe-lab-starter/
├── api/   # .NET 10 Web API
└── web/   # Angular 18 application
```

Run the API and web client from the selected Vibe Coding lab in separate terminals:

```bash
cd "Vibe Coding/15-vibe-lab-starter/api/CatalogApi"
dotnet run
```

```bash
cd "Vibe Coding/15-vibe-lab-starter/web"
npm install
npm start
```

The default development URLs are `http://localhost:5144` for the API and
`http://localhost:4200` for Angular. Check the selected lab README before running commands because
some labs intentionally contain defects or focus on tests and refactoring.

## Working principles

- Understand the problem and design before asking an AI tool to implement it.
- Provide focused context instead of dumping the whole repository.
- Review, validate, and explain every generated diff before accepting it.
- Keep controllers thin, put contracts in DTOs, and keep UI services responsible for HTTP state.
- Never place credentials, API keys, tokens, or other secrets in prompts or source files.
- Commit before large experiments so Git remains an undo button.

## Getting started

1. Choose the lab that matches your current day or topic.
2. Read that lab's `README.md` and task sheet before editing code.
3. Run the baseline application or tests.
4. Complete the exercise, inspect the diff, and verify the stated done-when criteria.

The repository is intentionally organized as independent lab starters rather than one application;
work inside the specific lab folder you selected.