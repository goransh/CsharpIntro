using System;

namespace WebApplication {
    public class Todo {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public Todo(string description) => Description = description;
    }
}
