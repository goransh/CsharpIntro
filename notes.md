#C# Introduction notes

## Basics

```csharp
// variables
int one = 1;
string hello = "Hello";
Console.WriteLine(hello + " World!"); // string concatenation 

// type inference
var two = 1;
var today = new DateTime(2021, 06, 15);
Console.WriteLine($"The current year is {today.Year}!"); // string interpolation

// loops
var numbers = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

// indexed for loop
for (var i = 0; i < numbers.Count; i++) {
    Console.WriteLine(numbers[i]);
}

// for each loop
foreach (var number in numbers) {
    Console.WriteLine(number);
}

// functional style loop
numbers.ForEach(Console.WriteLine);

// (roughly) the same as:
numbers.ForEach(number => Console.WriteLine(number));
```

## Classes
```csharp
public interface IPerson {
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public string SayHello();
}

public class Person : IPerson {
    public string? Name { get; set; } // = null; (reference type)
    public virtual DateTime Birthday { get; set; } // = null; doesn't work since it's value type (struct)
    public virtual string SayHello() => $"Hello, my name is {Name}";
}
```

* properties and methods
* initializers 
* interfaces
* nullable reference types and value types

```csharp
public class Developer : Person {

    private static readonly DateTime MinDate = new DateTime(1970, 01, 01);
    public override DateTime Birthday { 
        set => base.Birthday = value < MinDate ? MinDate : value;
    }

    public IPerson? Leader { get; set; }

    // backing field
    private int _linesOfCodePerDay = 0;

    public int LinesOfCodePerDay {
        get => _linesOfCodePerDay;
        set => _linesOfCodePerDay = Math.Max(0, value);
    }

    public TimeSpan Age => DateTime.Now - Birthday;

    // same as:
    // public TimeSpan Age {
    //     get { return DateTime.Now - Birthday; }
    // }

    public DeveloperType Type { get; set; }

    public void Consume(int amount, Beverage beverage) {
        var linesPerDayBoost = beverage switch {
            Beverage.Coffee => 10,
            Beverage.Tea => 5,
            Beverage.EnergyDrink => 100,
            _ => throw new ArgumentOutOfRangeException(nameof(beverage), beverage, null),
        };

        LinesOfCodePerDay += linesPerDayBoost * amount;
    }

    // same as:
    // public void Consume(int amount, Beverage beverage) {
    //     int linesPerDayBoost;
    //     switch (beverage) {
    //         case Beverage.Coffee:
    //             linesPerDayBoost = 10;
    //             break;
    //         case Beverage.Tea:
    //             linesPerDayBoost = 5;
    //             break;
    //         case Beverage.EnergyDrink:
    //             linesPerDayBoost = 100;
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException(nameof(beverage), beverage, null);
    //     }
    //
    //     LinesOfCodePerDay += linesPerDayBoost * amount;
    // }

    public override string SayHello() => $"{base.SayHello()} and I'm a {Type:G} developer!";

    // same as:
    // public string SayHello() {
    //     return $"Hello, my name is {Name} and I'm a {Type:G} developer!";
    // }

    public string SayLeaderName() {
        // null conditional and null coalescing operators
        var leaderName = Leader?.Name ?? "<unknown>";
        return $"My leader's name is {leaderName}!";
    }
}

public enum Beverage {
    Coffee,
    Tea,
    EnergyDrink,
}

public enum DeveloperType {
    Unspecified,
    Backend,
    Frontend,
    FullStack,
}
```

* inheritance, virtual, base
* custom getters and setters

## Initializers

```csharp
public static class Initializers {
    public static void Run() {
        // collection initializers
        var numbers = new List<int> {1, 2, 3};
        var strings = new List<string> {"A", "List", "of", "Strings"};

        // traditional constructors works as expected
        var myObject = new MyClass("Some Value");

        // but in C# we prefer object initializers
        var developer = new Developer {
            Name = "Ann",
            Birthday = new DateTime(1994, 12, 01),
            Type = DeveloperType.FullStack,
        };

        // dictionary (map) initializer
        var dict = new Dictionary<string, IPerson> {
            {"ann", developer}, // {key, value} 
            {"john", new Person {Name = "John"}},
        };

        var ann = dict["ann"];
        var annSafe = dict.GetValueOrDefault("ann");
    }
}

class MyClass {
    public string Value { get; set; }
    public MyClass(string value) => Value = value;
}
```

## Extension methods

```csharp
public static class StringExtensions {
    /// <summary>
    /// Returns a string containing this string repeated n times.
    /// </summary>
    /// <param name="str">the string to repeat</param>
    /// <param name="n">the number of times to repeat the string</param>
    /// <returns>the repeated string</returns>
    public static string Repeat(this string str, int n) =>
        new StringBuilder(str.Length * n)
            .Insert(0, str, n)
            .ToString();
}
```

## BasicLinq

```csharp
var developers = Data.Developers;

// filter
var frontendDevs = developers.Where(developer => developer.Type == DeveloperType.Frontend);

// map
var names = developers.Select(developer => developer.Name);

// sorted
var orderedByName = developers.OrderBy(developer => developer.Name);

var thirtyYears = DateTime.Now - DateTime.Now.AddYears(-30);

// chaining
var namesUnder30 = developers
    .Where(developer => developer.Age < thirtyYears)
    .OrderBy(developer => developer.Age)
    .Select(developer => developer.Name);

// alternative linq syntax
var namesOverThirty =
    from dev in developers
    where dev.Age >= thirtyYears
    orderby dev.Age
    select dev.Name;

// grouping
var groupedByType = developers.GroupBy(developer => developer.Type);

foreach (var grouping in groupedByType) {
    var namesJoined = string.Join(", ", grouping.Select(developer => developer.Name));
    Console.WriteLine($"{grouping.Key:G} developers: {namesJoined}");
}
```

## LazyLinq

```csharp
public static void Run() {
    var developers = Data.Developers;

    var computed = developers.Select(DoSomeHeavyComputation);

    foreach (var value in computed) {
        Console.WriteLine($"The computed value was {value}");
    }

    var sum = computed.Sum();
    Console.WriteLine($"The sum is {sum}");

    foreach (var value in computed.Take(3)) {
        Console.WriteLine($"The computed value was {value}");
    }
}

public static int DoSomeHeavyComputation(Developer developer) {
    Console.WriteLine($"Doing some heavy computation for {developer.Name}!");
    return new Random(developer.LinesOfCodePerDay).Next() % 1_000;
}
```

## Advanced linq

```csharp
var developers = Data.Developers;

// anonymous types
var anonymousDevelopers = developers.Select(
    developer => new {
        developer.Type,
        IsProductive = developer.LinesOfCodePerDay > 50,
    }
);

foreach (var anonymousDeveloper in anonymousDevelopers) {
    Console.WriteLine(
        $"A {anonymousDeveloper.Type:G} developer is{(anonymousDeveloper.IsProductive ? "" : " not")} productive"
    );
}

// group by multiple properties
var groupedByTypeAndBirthYear =
    developers.GroupBy(developer => new {developer.Type, developer.Birthday.Year});

var developersByType = developers
    .GroupBy(developer => developer.Type)
    .ToImmutableDictionary(
        group => group.Key,
        group => group.ToImmutableList()
    );

var fullStackDevelopers = developersByType[DeveloperType.FullStack];

// destructuring tuples
foreach (var (type, list) in developersByType) {
    var namesJoined = string.Join(", ", list.Select(developer => developer.Name));
    Console.WriteLine($"{type:G} developers: {namesJoined}");
}

// pattern matching
var productiveBackendDevelopers =
    developers.Where(developer => developer is {Type: DeveloperType.Backend, LinesOfCodePerDay: >50});

// parallel
var average = developers
    .AsParallel()
    .Select(LazyLinq.DoSomeHeavyComputation)
    .Average();

```