using System;
using System.Collections.Generic;

namespace CsharpIntro {
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
        }
    }

    class MyClass {
        public string Value { get; set; }
        public MyClass(string value) => Value = value;
    }
}
