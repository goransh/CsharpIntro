using System;

namespace CsharpIntro {
    public class Developer {
        public string Name { get; set; }
        public DateTime BirthDay { get; set; } // = null; doesn't work since it's value type (struct)

        // backing field
        private int _energy = 0;

        public int Energy {
            get => _energy;
            set => _energy = Math.Max(0, value);
        }

        public TimeSpan Age => DateTime.Now - BirthDay;
        
        public DeveloperType Type { get; set; }

        public void Consume(int amount, Beverage beverage) {
            var energyGain = beverage switch {
                Beverage.Coffee => 10,
                Beverage.Tea => 5,
                Beverage.EnergyDrink => 100,
                _ => throw new ArgumentOutOfRangeException(nameof(beverage), beverage, null)
            };

            Energy += energyGain * amount;
        }

        // public void Consume(int amount, Beverage beverage) {
        //     int energyGain;
        //     switch (beverage) {
        //         case Beverage.Coffee:
        //             energyGain = 10;
        //             break;
        //         case Beverage.Tea:
        //             energyGain = 5;
        //             break;
        //         case Beverage.EnergyDrink:
        //             energyGain = 100;
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(beverage), beverage, null);
        //     }
        //
        //     Energy += energyGain * amount;
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
        Fullstack,
    }
}
