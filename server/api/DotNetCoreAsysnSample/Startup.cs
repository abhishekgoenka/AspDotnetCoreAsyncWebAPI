using DotNetCoreAsysnSample.Infrastructure.Filters;
using DotNetCoreAsysnSample.Repository;
using DotNetCoreShared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

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
            // services.AddDbContext<CustomersDbContext>(context =>
            // {
            //     context.UseSqlServer(Configuration.GetConnectionString("CustomersSqlServerConnectionString"));
            // });

            //Add SqLite support
            services.AddDbContext<ApplicationDbContext>(context =>
            {
                context.UseSqlite(Configuration.GetConnectionString("sqliteConnectionString"));
            });

            // Add framework services.
            services.AddMvc(options => { options.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
                .AddControllersAsServices(); //Injecting Controllers themselves through DIFor further info see: http://docs.autofac.org/en/latest/integration/aspnetcore.html#controllers-as-services


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
            services.AddTransient<SampleClass>();

            //todo: Add resilience framework for .NET Core like Polly, if using external service
            // https://www.hanselman.com/blog/AddingResilienceAndTransientFaultHandlingToYourNETCoreHttpClientWithPolly.aspx

            //https://github.com/domaindrivendev/Swashbuckle.AspNetCore
            //https://localhost:5000/swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET Core Customers API",
                    Description = "ASP.NET Core API Documentation",
                    TermsOfService = new System.Uri("https://twitter.com/govindkaran"),
                    Contact = new OpenApiContact { Name = "Abhishek Goenka", Url = new System.Uri("https://twitter.com/govindkaran") },
                    License = new OpenApiLicense { Name = "MIT", Url = new System.Uri("https://en.wikipedia.org/wiki/MIT_License") }
                });

                //Add XML comment document by uncommenting the following
                // var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MyApi.xml");
                // options.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            CustomersDbSeeder customersDbSeeder)
        {
            // http://www.binaryintellect.net/articles/f4e492f7-9eec-46ba-b316-0584907e3e84.aspx
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();


            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            // Visit http://localhost:5000/swagger
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core Customers API"); });

            // Add this line; you'll need `using Serilog;` up the top, too
            app.UseSerilogRequestLogging();

            app.UseRouting();

            //disable CORS
            app.UseCors("AllowAnyOrigin");

            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });

            // Use the Serilog request logging middleware to log HTTP requests.
            app.UseSerilogRequestLogging();

            //seed data
            customersDbSeeder.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}