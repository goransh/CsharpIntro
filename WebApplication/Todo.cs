namespace WebApplication {
    /*
     * C# 9 introduced records which are classes that automatically implement
     * Equals and HashCode. They are immutable by default and have a simplified syntax
     * for defining properties and constructors.
     */
    public record Todo(
        int Id,
        int UserId,
        string Title,
        bool Completed = false
    );
}
