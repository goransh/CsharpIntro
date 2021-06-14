using System;
using System.Collections.Immutable;
using System.Linq;

namespace CsharpIntro {
    public static class AdvancedLinq {
        public static void Run() {
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
        }
    }
}
