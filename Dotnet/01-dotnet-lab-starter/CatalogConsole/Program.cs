// Day 1 — Catalog console. Your job: turn this skeleton into the menu app in TASKS.md.
//
// The shape you're building:
//   1) a Product record — Id, Name, Price, Category — that also implements IDiscountable
//   2) a List<Product> holding the whole catalog
//   3) a menu loop — Add / List / Average / Find / Exit, driven by a switch
//   4) a FindById helper the Find option leans on

// TODO (you) — Day 1: create the List<Product> that holds the catalog, plus an int nextId
// counter — there's no database yet to hand out ids, so you assign them yourself (nextId++).
// TODO (you) — Day 1: the menu loop.
// while (true) { print the menu, Console.ReadLine() the choice, switch on it }:
//   "1"    — ask for Name, Price, Category; add a new Product to the list
//   "2" List productList — print every product: #id, name, price, category
//   "3" Average price — LINQ: productList.Average(p => p.Price) (guard the empty catalog first)
//   "4" Find product  — read an id, look it up with FindById, print it (never crash)
//   "5" Exit          — return


Console.WriteLine("Catalog console — menu goes here");
// TODO (you) — Day 1: the shared lookup the Find option reuses. A one-liner over the list:

// TODO (you) — Day 1: declare the interface and the record here. (Type declarations live
// BELOW the top-level statements — the compiler insists.) An interface is a PROMISE about
// what a type can do; a record gives you a constructor, value equality and a readable
// ToString for free. Have Product fulfil the IDiscountable promise:
//
var productList= new List<Product>();
int currId=0;
while (true)
{
    Console.WriteLine("Please select mode");
    string input =Console.ReadLine()??"";
    switch (input){
        case "Add product":
            try{
                AddProduct();
            }
            catch
            {
                Console.WriteLine("Price must be a number");
            }
            break;
        case "Edit product":
            try{
                EditProduct();
            }
            catch
            {
                Console.WriteLine("Price must be a number");
            }
            break;
        case "List productList":
            PrintList();
            break;
        case "List sortedproductList":
            PrintListSorted();
            break;
        case "Average price":
            GetAvgPrice();
            break;
        case "Find product":
            try{
                FindAProduct();
            }
            catch (Exception  e)
            {
                Console.WriteLine($"The product doesn't exist.");
            }
            
            break;
        case "Delete product":
            try{
                DeleteProduct();
            }
            catch (Exception  e)
            {
                Console.WriteLine($"The product doesn't exist.");
            }
            
            break;
        case "Apply discount":
            try{
                Discount();
            }
            catch (Exception  e)
            {
                Console.WriteLine($"The product doesnt exist and Discount and id must be a number");
            }
            
            break;
        case "Exit":
            return;
        default:
            Console.WriteLine("Enter a valid mode");
            break;

    }
}



// 1: Add new Product
void AddProduct()
{
    Console.WriteLine("Please enter product name:");
    string Name=Console.ReadLine()??"";
    Console.WriteLine("Please enter product Price:");
    string s=Console.ReadLine()??"";
    decimal Price=0;
    Price=decimal.Parse(s); 
    while (Price < 0)
    {    
        Console.WriteLine("Please enter a valid product Price:");
        Price=decimal.Parse(Console.ReadLine()??"");
    }
    Console.WriteLine("Please enter product Category:");
    string Category=Console.ReadLine()??"";
    var product=new Product(currId+1,Name,Price,Category);
    currId++;
    productList.Add(product);
}


// 2: Print The List
void PrintList()
{
    foreach (var product in productList){
        Console.WriteLine($"Product ID:{product.Id} productroduct Name:{product} Product Price:{product.Price}");
    }
}


// 3: Get The Avarage Price
void GetAvgPrice()
{
    decimal totalPrice=0;
    foreach(var p in productList)
    {
        totalPrice=totalPrice+p.Price;
    }
    Console.WriteLine($"Avarage price={totalPrice/productList.Count}");
}


// 4: Find Priduct
void FindAProduct()
{
    Console.WriteLine("Please enter product id:");
    int id= int.Parse(Console.ReadLine()??"");
    if (id < 0)
    {
        Console.WriteLine("id must be +ve");
        return;
    }
    var p=FindById(id);
    Console.WriteLine($"Product ID:{p.Id} Product Name:{p.Name} Product Price:{p.Price}");
    
}

// 5: find product and apply discount
void Discount()
{
    Console.WriteLine("Please enter product id:");
    int id= int.Parse(Console.ReadLine()??"");
    if (id < 0)
    {
        Console.WriteLine("id must be +ve");
        return;
    }
    var p=FindById(id);
    Console.WriteLine("Please enter product discount:");
    int discount= int.Parse(Console.ReadLine()??"");
    if (discount < 0)
    {
        Console.WriteLine("discount must be +ve");
        return;
    }
    Console.WriteLine($"Price after Discount={p.PriceAfter(discount)}");

}


// 6: Print The List sorted
void PrintListSorted()
{
    var sorted = productList.OrderBy(x => x.Price);
    foreach (var product in sorted){
        Console.WriteLine($"Product ID:{product.Id} productroduct Name:{product} Product Price:{product.Price}");
    }
}

// 7: Delete Product
void DeleteProduct()
{
    Console.WriteLine("Please enter product id:");
    int id= int.Parse(Console.ReadLine()??"");
    if (id < 0)
    {
        Console.WriteLine("id must be +ve");
        return;
    }
    var p=FindById(id);
    productList.Remove(p);
}

// 8: Edit Product 
void EditProduct()
{
    Console.WriteLine("Please enter product id:");
    int id= int.Parse(Console.ReadLine()??"");
    if (id < 0)
    {
        Console.WriteLine("id must be +ve");
        return;
    }
    var p=FindById(id);
    Console.WriteLine("Please enter product name:");
    string name=Console.ReadLine()??"";
    Console.WriteLine("Please enter product Price:");
    string s=Console.ReadLine()??"";
    decimal price=0;
    price=decimal.Parse(s); 
    while (price < 0)
    {    
        Console.WriteLine("Please enter a valid product price:");
        price=decimal.Parse(Console.ReadLine()??"");
    }
    Console.WriteLine("Please enter product Category:");
    string category=Console.ReadLine()??"";
    var newName=name!=""?name:p.Name;
    var newPrice=price!=0?price:p.Price;
    var newCategory=category!=""?category:p.Category;
    var updatedP=p with {Price=newPrice,Name=newName,Category=newCategory};
    productList.Remove(p);
    productList.Add(updatedP);

}
Product? FindById(int id) => productList.Find(p => p.Id == id);




public interface IDiscountable
{
    decimal PriceAfter(decimal percentOff);
}

public record Product(int Id, string Name, decimal Price, string Category) : IDiscountable
{
    public decimal PriceAfter(decimal percentOff) => Math.Round(Price * (1 - percentOff / 100m), 2);
}
