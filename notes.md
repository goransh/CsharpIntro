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
public class Developer {
    public string Name { get; set; } = null;
    public DateTime Birthday { get; set; } // = null; doesn't work since it's value type (struct)

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

    public string SayHello() => $"Hello, my name is {Name} and I'm a {Type:G} developer!";

    // same as:
    // public string SayHello() {
    //     return $"Hello, my name is {Name} and I'm a {Type:G} developer!";
    // }
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

## Inheritance and nullable reference types

```csharp

public class Laptop {
    public string Id { get; set; }

    // nullable reference types
    public Developer? Owner { get; set; }

    public virtual string? Type { get; } = null;

    // method using null coalescing operators
    public virtual string GetOwnerString() {
        var ownerName = Owner?.Name ?? "<Unknown>";
        return $"Laptop with Id {Id} is owned by {ownerName}";
    }
}

class WindowsLaptop : Laptop {
    // override function, calling the base implementation
    public override string GetOwnerString() => $"Windows {base.GetOwnerString()}";

    // override property
    public override string? Type => "Windows";
}
```

## Initializers

```csharp
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
var dict = new Dictionary<string, Developer> {
    {"joe", developer}, // {key, value} 
    {
        "kari",
        new Developer {
            Name = "John",
            Birthday = new DateTime(2001, 03, 15),
            LinesOfCodePerDay = 100,
            Type = DeveloperType.Backend,
        }
    },
};

var joe = dict["joe"];
var joeSafe = dict.GetValueOrDefault("joe");

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
    // warning: linq is lazy

    var computed = developers.Select(DoSomeHeavyComputation);

    foreach (var value in computed) {
        Console.WriteLine($"The computed value was {value}");
    }

    var sum = computed.Sum();
    Console.WriteLine($"The sum is {sum}");

    var average = developers
        .AsParallel()
        .AsOrdered()
        .Select(DoSomeHeavyComputation)
        .Take(3)
        .Average();
    Console.WriteLine($"The average value is {average}");
}

private static int DoSomeHeavyComputation(Developer developer) {
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
        HasEnergy = developer.LinesOfCodePerDay > 0,
    }
);

foreach (var anonymousDeveloper in anonymousDevelopers) {
    Console.WriteLine(
        $"A {anonymousDeveloper.Type:G} developer has{(anonymousDeveloper.HasEnergy ? "" : " no")} energy"
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

// advanced: pattern matching
var backendDevsWithEnergy =
    developers.Where(developer => developer is {Type: DeveloperType.Backend, LinesOfCodePerDay: >50});

```