global using toDo_API.Services.UserService;
using toDo_API.db;
using toDo_API.Models;
using toDo_API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
namespace toDo_API
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var configManager = builder.Configuration;

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token})",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        "my secret key ntasdfhkjasdjkhflasdfhjkdsfahjkl"
                    )),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var config = new ApplicationConfig();
            configManager.Bind("Application", config);

            builder.Services.AddDbContext<ApplicationContext>();
            builder.Services.AddScoped<ApplicationConfig>(provider => config);
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddHttpContextAccessor();
        }
    }
}