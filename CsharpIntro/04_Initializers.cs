using System;
using System.Collections.Generic;

namespace CsharpIntro {
    public static class Initializers {
        public static void Run() {
        }
    }

    class MyClass {
        public string Value { get; set; }
        public MyClass(string value) => Value = value;
    }
}
