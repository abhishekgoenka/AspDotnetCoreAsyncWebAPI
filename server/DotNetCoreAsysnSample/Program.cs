using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace DotNetCoreAsysnSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(string.Format("appsettings.{0}.json", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"), optional: true, reloadOnChange: true)
                .AddUserSecrets<Startup>(optional: true, reloadOnChange: true).Build();

            // for sql server
            //Log.Logger = new LoggerConfiguration().WriteTo.MSSqlServer(connectionString: configuration.GetConnectionString("SerilogConnection"), restrictedToMinimumLevel: LogEventLevel.Information, sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true }).WriteTo.Console().CreateLogger();

            // add SQLite logger
            Log.Logger = new LoggerConfiguration().WriteTo.SQLite(sqliteDbPath: $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}{configuration.GetConnectionString("sqliteDbPath")}").WriteTo.Console().CreateLogger();

            CreateWebHostBuilder(args).UseSerilog().Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>();
    }
}