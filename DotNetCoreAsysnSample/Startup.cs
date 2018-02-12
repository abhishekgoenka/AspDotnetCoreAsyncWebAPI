using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DotNetCoreAsysnSample.Repository;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreAsysnSample
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
            //Add SQL Server support
            services.AddDbContext<CustomersDbContext>(context =>
            {
                context.UseSqlServer(Configuration.GetConnectionString("CustomersSqlServerConnectionString"));
            });

            //Add SqLite support
            //services.AddDbContext<CustomersDbContext>(context =>
            //{
            //    context.UseSqlite(Configuration.GetConnectionString("CustomersSqliteConnectionString"));
            //});

            services.AddMvc();

            //allow any origin
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });

            services.AddScoped<ICustomersRepositoryAsync, CustomersRepositoryAsync>();
            services.AddTransient<CustomersDbSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            CustomersDbSeeder customersDbSeeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //disable CORS
            app.UseCors("AllowAnyOrigin");
            app.UseMvc();

            //seed data
            customersDbSeeder.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}
