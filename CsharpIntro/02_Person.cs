using System;

namespace CsharpIntro {

    public interface IPerson {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string SayHello();
    }

    public class Person : IPerson {
        public string? Name { get; set; } // = null; (reference type)
        public DateTime Birthday { get; set; } // = null; doesn't work since it's value type (struct)
        public virtual string SayHello() => $"Hello, my name is {Name ?? "<REDACTED>"}";
    }
}
