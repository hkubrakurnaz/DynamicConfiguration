using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Configuration.Api.Middleware;
using Configuration.Application.Configurations.Validators;
using Configuration.Domain.Db;
using Configuration.Infrastructure.Db;
using Configuration.Infrastructure.Db.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;

namespace Configuration.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Configuration.Api", Version = "v1" });
            });

            services.AddMediatR(GetProjectAssemblies().ToArray());
            
            services.AddSingleton(new ConfigurationDbContext(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Configuration.Api v1"));
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        List<Assembly> GetProjectAssemblies()
        {
            var startupAssembly = typeof(Program).Assembly;
            var list = new List<Assembly> { startupAssembly };
            
            list.AddRange(
                startupAssembly.GetReferencedAssemblies()
                    .Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith("Configuration"))
                    .GroupBy(x => x.Name)         
                    .Select(g => g.First())          
                    .Select(Assembly.Load)          
            );

            return list;
        }

        public partial class Program
        {
        }
    }
}