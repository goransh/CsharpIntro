using System;
using System.Collections.Generic;

namespace CsharpIntro {
    public static class Data {
        public static readonly IReadOnlyCollection<Developer> Developers = new List<Developer> {
            // new Developer {
            //     Name = "Ann",
            //     Birthday = new DateTime(1994, 12, 01),
            //     Type = DeveloperType.FullStack,
            // },
            // new Developer {
            //     Name = "John",
            //     Birthday = new DateTime(2001, 03, 15),
            //     LinesOfCodePerDay = 100,
            //     Type = DeveloperType.Backend,
            // },
            // new Developer {
            //     Name = "Marie",
            //     Birthday = new DateTime(1985, 08, 06),
            //     LinesOfCodePerDay = -10,
            //     Type = DeveloperType.Backend,
            // },
            // new Developer {
            //     Name = "Bob",
            //     Birthday = new DateTime(1970, 02, 09),
            //     Type = DeveloperType.Frontend,
            // },
            // new Developer {
            //     Name = "Monica",
            //     Birthday = new DateTime(1979, 02, 21),
            //     Type = DeveloperType.Frontend,
            // },
            // new Developer {
            //     Name = "Lucas",
            //     Birthday = new DateTime(1999, 05, 05),
            //     LinesOfCodePerDay = 51,
            //     Type = DeveloperType.Backend,
            // },
        };
    }
}
