using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpIntro {
    public static class BasicLinq {
        public static void Run() {
            var developers = Data.Developers;

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
        }
    }
}
