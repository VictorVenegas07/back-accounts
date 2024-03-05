using Aplication;
using back_accounts.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
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

                    Console.WriteLine($"An error occurred in ScopeWeb: {e.Message}");
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Prueba tenica Inteia API",
                    Version = "v1",
                    Description = "API para gestionar proveedores",
                    Contact = new OpenApiContact
                    {
                        Name = "Victor Venegas",
                        Email = "victorvenegas07@email.com",
                        Url = new Uri("https://github.com/VictorVenegas07"),
                    },
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                        {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                       {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                        }
                                },
                            new string[] { }
                        }
                    });
            });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.useErrorHandlingMiddleware();

            app.MapControllers();

        }
    }
}
