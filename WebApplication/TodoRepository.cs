using System.Collections.Immutable;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApplication {
    public class TodoRepository {
        private static readonly HttpClient HttpClient = new HttpClient();

        private readonly ILogger<TodoRepository> _logger;

        public TodoRepository(ILogger<TodoRepository> logger) {
            _logger = logger;
        }

        public async Task<IImmutableList<Todo>> ListTodos() {
            // await async API call 
            var response = await HttpClient
                .GetFromJsonAsync<IImmutableList<Todo>>("https://jsonplaceholder.typicode.com/todos");

            if (response is null) {
                _logger.LogError("Fetch of Todos failed");
                return ImmutableList<Todo>.Empty;
            }

            return response;
        }
    }
}
