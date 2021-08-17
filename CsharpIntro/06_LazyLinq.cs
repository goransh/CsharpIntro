using System;
using System.Linq;

namespace CsharpIntro {
    public static class LazyLinq {
        public static void Run() {
            var developers = Data.Developers;
        }

        // public static int DoSomeHeavyComputation(Developer developer) {
        //     Console.WriteLine($"Doing some heavy computation for {developer.Name}!");
        //     return new Random(developer.LinesOfCodePerDay).Next() % 1_000;
        // }
    }
}
