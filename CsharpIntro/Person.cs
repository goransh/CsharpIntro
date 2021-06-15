namespace CsharpIntro {

    public interface IPerson {
        public string Name { get; set; }
        public string SayHello();
    }

    public class Person : IPerson {
        public virtual string Name { get; set; }
        public virtual string SayHello() => $"Hello, my name is {Name}";
    }
}
