//The task is to turn all if these into LINQ operations



//Exercise 1 — Flatten nested collections
var numbers = new List<List<int>>
{
    new() { 1, 2, 3 },
    new() { 4, 5 },
    new() { 6, 7, 8, 9 }
};

var allNumbers = new List<int>();

allNumbers = numbers.SelectMany(number => number).ToList();

foreach (var list in numbers)
{
    foreach (var n in list)
    {
        allNumbers.Add(n);
    }
}

// /// 
// /// 
// /// Exercise 2 — Files inside directories
// /// 
// var allFiles = new List<string>();

// foreach (var dir in Directory.GetDirectories("stores"))
// {
//     foreach (var file in Directory.GetFiles(dir, "*.json"))
//     {
//         allFiles.Add(file);
//     }
// }
// /////////////////Exercise 3 — Filtering while flattening

// var expensiveSales = new List<Sale>();

// foreach (var store in stores)
// {
//     foreach (var sale in store.Sales)
//     {
//         if (sale.Amount > 1000)
//         {
//             expensiveSales.Add(sale);
//         }
//     }
// }

// /////xercise 4 — Projection using outer + inner values
// var results = new List<string>();

// foreach (var store in stores)
// {
//     foreach (var employee in store.Employees)
//     {
//         results.Add($"{store.Name} - {employee.Name}");
//     }
// }


// ////Exercise 5 — Many-to-many expansion

// var combinations = new List<(string store, string category)>();

// foreach (var store in stores)
// {
//     foreach (var category in categories)
//     {
//         combinations.Add((store.Name, category));
//     }
// }
