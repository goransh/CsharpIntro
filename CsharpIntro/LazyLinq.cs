using System;
using System.Linq;

namespace CsharpIntro {
    public static class LazyLinq {
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
    }
}
