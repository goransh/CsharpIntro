using System;

namespace CsharpIntro {
    class Program {
        static void Main(string[] args) {
            // Console.WriteLine("Hello World!");
            
            Basics.Run();
            Initializers.Run();
            BasicLinq.Run();
            AdvancedLinq.Run();
            LazyLinq.Run();
            Console.WriteLine("Hello ".Repeat(10));
            
        }
    }
}
