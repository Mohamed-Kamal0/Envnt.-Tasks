// Day 1 — Catalog console (solution).
// A record, an interface, a List<T>, and a switch-driven menu. Prices are money, so Price
// is a decimal; the average is one line of LINQ, not a hand-rolled loop and counter.

var products = new List<Product>();
var nextId = 1; // the list has no database to hand out ids, so we do it ourselves

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== Catalog ===");
    Console.WriteLine("1) Add product");
    Console.WriteLine("2) List products");
    Console.WriteLine("3) Average price");
    Console.WriteLine("4) Find product by id");
    Console.WriteLine("5) Exit");
    Console.Write("> ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": AddProduct(); break;
        case "2": ListProducts(); break;
        case "3": ShowAveragePrice(); break;
        case "4": FindProduct(); break;
        case "5": return;
        default: Console.WriteLine("Unknown option — pick 1-5."); break;
    }
}

void AddProduct()
{
    Console.Write("Name: ");
    var name = Console.ReadLine() ?? "";
    Console.Write("Price: ");
    var priceText = Console.ReadLine() ?? "";
    Console.Write("Category: ");
    var category = Console.ReadLine() ?? "";

    if (name == "")
    {
        Console.WriteLine("Name is required.");
        return;
    }

    // decimal.TryParse guards against "banana" — a message, never a crash
    if (!decimal.TryParse(priceText, out var price) || price <= 0)
    {
        Console.WriteLine("Price must be a positive number.");
        return;
    }

    products.Add(new Product(nextId++, name, price, category));
    Console.WriteLine($"Added \"{name}\".");
}

void ListProducts()
{
    if (products.Count == 0)
    {
        Console.WriteLine("The catalog is empty — add a product first.");
        return;
    }

    foreach (var product in products)
        // string interpolation + the :C currency format — no manual "$" concatenation
        Console.WriteLine($"#{product.Id} {product.Name} — {product.Price:C} ({product.Category})");
}

void ShowAveragePrice()
{
    if (products.Count == 0)
    {
        Console.WriteLine("The catalog is empty — add a product first.");
        return;
    }

    // LINQ: one line for the average instead of a loop, a running total and a counter
    var average = products.Average(p => p.Price);
    Console.WriteLine($"Average price: {average:C}");
}

void FindProduct()
{
    Console.Write("Id to find: ");
    var idText = Console.ReadLine() ?? "";
    if (!int.TryParse(idText, out var id))
    {
        Console.WriteLine("That isn't a number.");
        return;
    }

    var product = FindById(id);
    if (product is null)
    {
        Console.WriteLine("No product with that id.");
        return;
    }

    Console.WriteLine($"#{product.Id} {product.Name} — {product.Price:C} ({product.Category})");
    // the interface at work: anything IDiscountable can quote a members' price
    Console.WriteLine($"  members pay {product.PriceAfter(10):C} (10% off)");
}

// Shared lookup — the Find option needs it, and pulling it out keeps the switch readable.
Product? FindById(int id) => products.Find(p => p.Id == id);

// An interface is a PROMISE about what a type can DO — here, "I can quote a discounted price."
// It says nothing about HOW; the record below decides that.
public interface IDiscountable
{
    decimal PriceAfter(decimal percentOff);
}

// A record: constructor, value equality, readable ToString — all for free. It also fulfils
// the IDiscountable promise, so a Product can quote its own sale price. Id/Name/Price/Category
// are the identity of the product, so they're positional; nothing here changes over its life.
public record Product(int Id, string Name, decimal Price, string Category) : IDiscountable
{
    public decimal PriceAfter(decimal percentOff) => Math.Round(Price * (1 - percentOff / 100m), 2);
}
