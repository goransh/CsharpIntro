using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication {
    [Controller]
    public class TodoController {
        private readonly ILogger<TodoController> _logger;
        private readonly TodoRepository _todoRepository;

        public TodoController(ILogger<TodoController> logger, TodoRepository todoRepository) {
            // dependency injection
            _logger = logger;
            _todoRepository = todoRepository;
        }

        [Route("/todos")]
        public async Task<IActionResult> ListTodos([FromQuery(Name = "completed")] bool completed = false) {
            _logger.LogInformation("Fetching Todos");
            var todos = await _todoRepository.ListTodos();

            // structured logging
            _logger.LogInformation("Received {NumberOfTodos} todos", todos.Count);

            var filtered = todos
                .Where(todo => todo.Completed == completed)
                .ToImmutableList();

            _logger.LogInformation("Will return {NumberOfFilteredTodos} todos", filtered.Count);

            return new OkObjectResult(filtered);
        }
    }
}
