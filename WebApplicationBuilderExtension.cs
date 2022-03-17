using toDo_API.db;
using toDo_API.Models;
using toDo_API.Repositories;
namespace toDo_API
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var configManager = builder.Configuration;

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var config = new ApplicationConfig();
            configManager.Bind("Application", config);

            builder.Services.AddDbContext<ApplicationContext>();
            builder.Services.AddScoped<ApplicationConfig>(provider => config);
            builder.Services.AddScoped<IGenericRepository, GenericRepository>();
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
        }
    }
}