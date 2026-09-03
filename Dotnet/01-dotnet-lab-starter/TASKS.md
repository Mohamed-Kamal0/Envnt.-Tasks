# Day 1 — Catalog console · Catalog tasks

**Start from:** `starter/` — it compiles and prints a placeholder; the TODOs inside
`CatalogConsole/Program.cs` are today's work. (No "yesterday" yet — day 1 is the bottom
rung of the ladder.)

## Before you start

- [ ] `dotnet --list-sdks` shows a **10.x** line (8.x alone won't build `net10.0`).
- [ ] `cd starter/CatalogConsole && dotnet run` prints `Catalog console — menu goes here`.

## Tasks

### 1 · The `Product` record + `IDiscountable`  ⏱ ~10
Declare `Product` at the bottom of `Program.cs`: positional `Id`, `Name`, `Price` (a
`decimal` — it's money), `Category`. Above it, declare an `interface IDiscountable` with one
method, `decimal PriceAfter(decimal percentOff)`, and have `Product` implement it (a members'
price is `Price * (1 - percentOff / 100)`).
- Hint: the exact shape is sketched in the TODO comment.
- **Done when:** the project compiles and you can say, out loud, why an interface is a
  *promise about what a type can do*, not *how* it does it.

### 2 · Add + List  ⏱ ~15
The menu loop: `while (true)` + `Console.ReadLine()` + a `switch`. Wire **Add product** (read
Name/Price/Category, `decimal.TryParse` the price, add a `new Product(nextId++, ...)` to the
`List<Product>`) and **List products** (print every product with its `#id`, name, `:C` price
and category).
- Hint: start with the loop and Exit only, then add one menu option at a time.
- **Done when:** you can add three products and see all three listed with their prices.

### 3 · Average price + Find by id  ⏱ ~15
**Average price** guards the empty catalog, then prints `products.Average(p => p.Price)` — one
line of LINQ. **Find product by id** reads an id, looks it up with a shared `FindById` helper,
and prints the product plus its 10%-off members' price (that's `IDiscountable` doing its job).
Missing id → a printed message, never a crash.
- Hint: `products.Find(p => p.Id == id)` returns `null` when there's no match — handle it.
- **Done when:** three products give a correct average; finding a real id prints it with the
  discount line; finding id `999` prints a message.

### 4 · Explain-every-line pass  ⏱ ~5
Read your `Program.cs` top to bottom and explain each line out loud (to a neighbor or to
yourself). Any line you can't explain, you redo — that's the month's rule.
- **Done when:** no line survives that you can't defend.

## Verify

```bash
cd starter/CatalogConsole
dotnet build          # 0 errors
dotnet run            # walk the menu: add 3, list, average, find 1, find 999, exit
```

---

`solution/` is for **after** an honest attempt — manual-first, AI explains only
([JUDGING.md](../../../JUDGING.md)).
