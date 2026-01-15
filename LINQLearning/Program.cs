//The task is to turn all if these into LINQ operations
//Learning both List Methods and queries style



//Exercise 1 — Flatten nested collections
var numbers = new List<List<int>>
{
    new() { 1, 2, 3 },
    new() { 4, 5 },
    new() { 6, 7, 8, 9 }
};

//

var allNumbers = numbers.SelectMany(numbers => numbers).Where(n => n > 5).ToList();
//allNumbers = [6,7,8,9]

IEnumerable<int> numberQuery =
    from rows in numbers
    from number in rows
    where number > 5
    select number;
//NumberQuery = [6,7,8,9]


/// 
/// 
/// Exercise 2 — Files inside directories
/// 
var allFiles = new List<string>();

allFiles = Directory.GetDirectories("stores").SelectMany(dir => Directory.GetFiles(dir, "*.json")).ToList();

var allFiles3 =
    from directory in Directory.GetDirectories("stores")
    from file in Directory.GetFiles(directory, "json.txt")
    select file;


// foreach (var dir in Directory.GetDirectories("stores"))
// {
//     foreach (var file in Directory.GetFiles(dir, "*.json"))
//     {
//         allFiles.Add(file);
//     }
// }
/////////////////Exercise 3 — Filtering while flattening

var expensiveSales = new List<Sale>();

var stores = new List<Store>
{
    new Store
    {
        Sales = new List<Sale>
        {
            new Sale { Amount = 500 },
            new Sale { Amount = 1500 },
            new Sale { Amount = 2000 }
        }
    },
    new Store
    {
        Sales = new List<Sale>
        {
            new Sale { Amount = 300 },
            new Sale { Amount = 1200 }
        }
    },
    new Store
    {
        Sales = new List<Sale>
        {
            new Sale { Amount = 800 },
            new Sale { Amount = 2500 }
        }
    }
};



var test = new Sale { Amount = 300 };

Random random = new Random();

for (int i = 0; i < random.Next(4, 6); i++)
{
    stores.Append(new Store
    {
        Sales = new List<Sale>(Enumerable.Range(0, 4)
    .Select(_ => new Sale { Amount = 100 })
    .ToList())
    });
}


expensiveSales = stores.SelectMany(store => store.Sales).Where(sale => sale.Amount > 1000).ToList();

expensiveSales.Clear();

foreach (var store in stores)
{
    foreach (var sale in store.Sales)
    {
        if (sale.Amount > 1000)
        {
            expensiveSales.Add(sale);
        }
    }
}

IEnumerable<int> expensiveSales2 =
    from store in stores
    from sale in store.Sales
    where sale.Amount > 5
    select sale.Amount;

/////exercise 4 — Projection using outer + inner values
var results = new List<string>();
stores.ForEach(store => store.Employees.AddRange(new[] 
{ 
    new Employee { Name = "Alice Johnson" },
    new Employee { Name = "Bob Smith" },
    new Employee { Name = "Carol White" }
}));

var storeNames = new List<string> { "Store A", "Store B", "Store C" };
for (int i = 0; i < stores.Count; i++)
{
    stores[i].Name = storeNames[i % storeNames.Count];
}

//With List methods: 

//Problem with needing both store.Name and all the employees in same scope can be solved with overloaded function or anonymous object mehod
//Overload SelectMany to keep the outer element while iterating the innter collection (employee)
results = stores
    .SelectMany(
        store => store.Employees,
        (store, employee) => $"{store.Name} - {employee.Name}"
    )
    .ToList();

//Anonymous object method uses a new {store, employee} to pass both values to next scope
results = stores
    .SelectMany(store => store.Employees.Select(employee => new { store, employee }))
    .Select(x => $"{x.store.Name} - {x.employee.Name}")
    .ToList();

//LINQ Query style
var results2 = 
    (from store in stores 
     from employee in store.Employees
     select $"{store.Name} - {employee.Name}")
    .ToList();


//With loops: 
foreach (var store in stores)
{
    foreach (var employee in store.Employees)
    {
        results.Add($"{store.Name} - {employee.Name}");
    }
}


////Exercise 5 — Many-to-many expansion
/// 
var categories = new List<string>(["Movies","Perfumes","Sport equipment"]);

var combinations = new List<(string store, string category)>();

combinations = (
    from store in stores 
    from category in categories 
    select (store.Name, category)
).ToList();

foreach (var store in stores)
{
    foreach (var category in categories)
    {
        combinations.Add((store.Name, category));
    }
}


class Sale()
{
    public int Amount { get; set; }
}

class Employee()
{
    public string Name { get; set; }
}

class Store()
{
    public string Name { get; set; }
    public List<Sale> Sales { get; set; } = [];
    public List<Employee> Employees { get; set; } = [];
}

