using System;
using System.Collections.Generic;

namespace CsharpIntro {
    public static class Basics {
        public static void Run() {
            // variables
            int one = 1;
            string hello = "Hello";
            Console.WriteLine(hello + " World!"); // string concatenation 
            
            // type inference
            var two = 1;
            var today = new DateTime(2021, 06, 15);
            Console.WriteLine($"The current year is {today.Year}!"); // string interpolation
            
            // loops
            var numbers = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            // indexed for loop
            for (var i = 0; i < numbers.Count; i++) {
                Console.WriteLine(numbers[i]);
            }

            // for each loop
            foreach (var number in numbers) {
                Console.WriteLine(number);
            }

            // functional style loop
            numbers.ForEach(Console.WriteLine);
            
            // (roughly) the same as:
            numbers.ForEach(number => Console.WriteLine(number));

        }
    }
}
