using System;
using System.Linq;

namespace CsharpIntro {
    public static class LazyLinq {
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
    }
}
