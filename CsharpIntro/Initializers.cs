using System;
using System.Collections.Generic;

namespace CsharpIntro {
    public static class Initializers {
        public static void Run() {
            // collection initializers
            var numbers = new List<int> {1, 2, 3};
            var strings = new HashSet<string> {"A", "Set", "of", "Strings"};

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
}
