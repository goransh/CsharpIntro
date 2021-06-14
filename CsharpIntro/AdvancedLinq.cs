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
                    IsProductive = developer.LinesOfCodePerDay > 50,
                }
            );

            foreach (var anonymousDeveloper in anonymousDevelopers) {
                Console.WriteLine(
                    $"A {anonymousDeveloper.Type:G} developer is{(anonymousDeveloper.IsProductive ? "" : " not")} productive"
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

            // pattern matching
            var productiveBackendDevelopers =
                developers.Where(developer => developer is {Type: DeveloperType.Backend, LinesOfCodePerDay: >50});
        }
    }
}
