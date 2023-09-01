using Aplication;
using back_accounts.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Seed;
using Shared;

namespace back_accounts
{
    public static class Startup
    {
        public static WebApplication Inicializar(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            ScopeWeb(app);
            return app;
        }
        private static async void ScopeWeb(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var userManager = service.GetRequiredService<UserManager<User>>();
                    var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(userManager, roleManager);
                    await DefaultBasicUser.SeedAsync(userManager, roleManager);

                }
                catch (Exception e)
                {

                    throw;
                }

            }
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddAplicationLayer();
            builder.Services.AddSharedInfraestructure(builder.Configuration);
            builder.Services.AddPersistenceInfraestructure(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddApiVersionExtensions();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.useErrorHandlingMiddleware();

            app.MapControllers();

        }
    }
}
