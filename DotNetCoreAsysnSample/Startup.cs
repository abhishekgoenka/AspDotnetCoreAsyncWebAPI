﻿using DotNetCoreAsysnSample.Infrastructure.Filters;
using DotNetCoreAsysnSample.Infrastructure.Handler;
using DotNetCoreAsysnSample.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddDbContext<CustomersDbContext>(context =>
            {
                context.UseSqlite(Configuration.GetConnectionString("CustomersSqliteConnectionString"));
            });

            // Add framework services.
            services.AddMvc(options => { options.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
                .AddControllersAsServices(); //Injecting Controllers themselves thru DIFor further info see: http://docs.autofac.org/en/latest/integration/aspnetcore.html#controllers-as-services


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

            //todo: Add resilience framework for .NET Core like Polly, if using external service
            // https://www.hanselman.com/blog/AddingResilienceAndTransientFaultHandlingToYourNETCoreHttpClientWithPolly.aspx

            //https://github.com/domaindrivendev/Swashbuckle.AspNetCore
            //https://localhost:5000/swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ASP.NET Core Customers API",
                    Description = "ASP.NET Core API Documentation",
                    TermsOfService = "None",
                    Contact = new Contact {Name = "Abhishek Goenka", Url = "https://twitter.com/govindkaran"},
                    License = new License {Name = "MIT", Url = "https://en.wikipedia.org/wiki/MIT_License"}
                });

                //Add XML comment document by uncommenting the following
                // var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MyApi.xml");
                // options.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            CustomersDbSeeder customersDbSeeder)
        {
            // http://www.binaryintellect.net/articles/f4e492f7-9eec-46ba-b316-0584907e3e84.aspx
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            // Visit http://localhost:5000/swagger
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core Customers API"); });

            //disable CORS
            app.UseCors("AllowAnyOrigin");
            app.UseMvc();

            //seed data
            customersDbSeeder.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}