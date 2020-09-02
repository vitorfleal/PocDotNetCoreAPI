using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AE.HealthSystem.Services.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services, IWebHostEnvironment _env)
        {
            services.AddSwaggerGen(s =>
            {
                string name;

                if (_env.IsDevelopment())
                {
                    name = "Health System API - Desenvolvimento";
                }
                if (_env.IsProduction())
                {
                    name = "Health System API - Produção";
                }
                else
                {
                    name = "Health System API - Homologação";
                }

                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = name,
                    Version = "v1",
                    Description = "API Health System",
                    Contact = new OpenApiContact
                    {
                        Name = "Vitor Leal",
                        Email = "vitorfleal@hotmail.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                    }
                });

                s.AddServer(new OpenApiServer { Url = "http://localhost:61545/" });
            });
        }
    }
}