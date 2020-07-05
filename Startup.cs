using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWebApplication2.Repositories.Postgres;
using MyWebApplication2.Services;

namespace MyWebApplication2
{
    public class Startup
    {
        private const string PostgresConnection = "Host=localhost;Port=5432;Database=classroom;Username=postgres;Password=postgres";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEntityFrameworkNpgsql().AddDbContext<MyWebApiContext>(options =>
            {
                options.UseNpgsql(PostgresConnection);
            });

            services.AddScoped<IStudentService, StudentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<MyWebApiContext>())
                {
                    Console.WriteLine("Running Postgres Migrations");
                    context.Database.MigrateAsync().GetAwaiter().GetResult();
                    Console.WriteLine("Completed Postgres Migrations");
                }
            }
        }
    }
}
