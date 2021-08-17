using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services
                .AddDbContext<TodoDatabaseContext>(
                    options => options.UseInMemoryDatabase("TodoDatabase")
                )
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TodoDatabaseContext databaseContext) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            InitTodos(databaseContext);
        }

        private static void InitTodos(TodoDatabaseContext context) {
            var todos = new List<Todo> {
                new("Learn C#"),
                new("Solve NP-complete problems"),
                new("End world hunger")
            };
            
            context.AddRange(todos);
            context.SaveChanges();
        }
    }
}
