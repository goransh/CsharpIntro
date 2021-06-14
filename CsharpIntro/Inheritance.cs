namespace CsharpIntro {
    public class Laptop {
        public string Id { get; set; }

        // nullable reference types
        public Developer? Owner { get; set; }

        public virtual string? Type { get; } = null;

        // method using null coalescing operators
        public virtual string GetOwnerString() {
            var ownerName = Owner?.Name ?? "<Unknown>";
            return $"Laptop with Id {Id} is owned by {ownerName}";
        }
    }

    class WindowsLaptop : Laptop {
        // override function, calling the base implementation
        public override string GetOwnerString() => $"Windows {base.GetOwnerString()}";

        // override property
        public override string? Type => "Windows";
    }
}
