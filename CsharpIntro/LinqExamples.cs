using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpIntro {
    public class LinqExamples {
        public static void Run() {
            var developers = new List<Developer> {
                new Developer {
                    Name = "Ann",
                    BirthDay = new DateTime(1994, 12, 01),
                    Type = DeveloperType.Fullstack
                },
                new Developer {
                    Name = "John",
                    BirthDay = new DateTime(2001, 03, 15),
                    Energy = 100,
                    Type = DeveloperType.Backend
                },
                new Developer {
                    Name = "Marie",
                    BirthDay = new DateTime(1985, 08, 06),
                    Energy = -10,
                    Type = DeveloperType.Backend
                },
                new Developer {
                    Name = "Bob",
                    BirthDay = new DateTime(1970, 02, 09),
                    Type = DeveloperType.Frontend
                },
                new Developer {
                    Name = "Monica",
                    BirthDay = new DateTime(1979, 02, 21),
                    Type = DeveloperType.Frontend
                },
                new Developer {
                    Name = "Lucas",
                    BirthDay = new DateTime(1999, 05, 05),
                    Energy = 51,
                    Type = DeveloperType.Unspecified
                }
            };

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

            // anonymous types
            var anonymousDevelopers = developers.Select(
                developer => new {
                    developer.Type,
                    HasEnergy = developer.Energy > 0
                }
            );

            foreach (var anonymousDeveloper in anonymousDevelopers) {
                Console.WriteLine(
                    $"A {anonymousDeveloper.Type:G} developer has{(anonymousDeveloper.HasEnergy ? "" : " no")} energy"
                );
            }

            // advanced: pattern matching
            var backendDevsWithEnergy =
                developers.Where(developer => developer is {Type: DeveloperType.Backend, Energy: >50});

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
            return new Random(developer.Energy).Next() % 1_000;
        }
    }
}
