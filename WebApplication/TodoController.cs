using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication {
    [Controller]
    [Route("/todos")]
    public class TodoController {
        private readonly ILogger<TodoController> _logger;
        private readonly TodoDatabaseContext _database;

        public TodoController(ILogger<TodoController> logger, TodoDatabaseContext database) {
            // dependency injection
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> ListTodos([FromQuery(Name = "completed")] bool? completed = null) {
            _logger.LogInformation(
                "Listing Todos where completed status is {CompletedStatus}",
                completed?.ToString() ?? "Ignored"
            );

            var queryable = completed is null
                ? _database.Todos
                : _database.Todos.Where(todo => todo.Completed == completed);

            var todos = await queryable
                .OrderBy(todo => todo.CreatedTime)
                .ToListAsync();

            // structured logging
            _logger.LogInformation("Found {NumberOfTodos} Todos", todos.Count);

            return new OkObjectResult(todos);
        }

        /// <summary>
        /// The save method is used to create new Todos or updated existing. It checks the Id of the input JSON
        /// to check if it finds an entity with that id in the database and if so will update it. If an entity doesn't
        /// exist, it will create one instead.
        /// </summary>
        /// <param name="newOrUpdatedTodo">The new or updated entity in the request body (JSON)</param>
        /// <returns>the updated entity</returns>
        [HttpPost]
        public async Task<IActionResult> SaveTodo([FromBody] Todo newOrUpdatedTodo) {
            var todo = await _database.Todos.FindAsync(newOrUpdatedTodo.Id);

            if (todo is null) {
                _logger.LogInformation("Adding new Todo with id {Id}", newOrUpdatedTodo.Id);
                await _database.Todos.AddAsync(newOrUpdatedTodo);
                todo = newOrUpdatedTodo;
            } else {
                _logger.LogInformation("Updating existing Todo with id {Id}", newOrUpdatedTodo.Id);
                todo.Description = newOrUpdatedTodo.Description;
                todo.Completed = newOrUpdatedTodo.Completed;
            }

            await _database.SaveChangesAsync();

            return new OkObjectResult(todo);
        }
    }
}
