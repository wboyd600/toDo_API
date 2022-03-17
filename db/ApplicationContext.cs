using Microsoft.EntityFrameworkCore;
using toDo_API.Models;

namespace toDo_API.db {
    public class ApplicationContext: DbContext
    {
        private readonly ApplicationConfig _applicationConfig;
        private string _connectionString => _applicationConfig.ConnectionString;
        public ApplicationContext(
            DbContextOptions options,
            ApplicationConfig applicationConfig
        ) 
            : base(options)
        {
            _applicationConfig = applicationConfig;
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql(_connectionString)
                .EnableSensitiveDataLogging()
                .UseSnakeCaseNamingConvention();
        }
    }
}