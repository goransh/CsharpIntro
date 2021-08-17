using Microsoft.EntityFrameworkCore;

namespace WebApplication {
    public class TodoDatabaseContext : DbContext {
        public TodoDatabaseContext(DbContextOptions<TodoDatabaseContext> options) : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
