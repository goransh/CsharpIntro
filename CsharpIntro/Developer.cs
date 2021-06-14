using System;

namespace CsharpIntro {
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
}
