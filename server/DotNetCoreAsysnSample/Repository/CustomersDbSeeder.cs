﻿using DotNetCoreAsysnSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreAsysnSample.Repository
{
    /// <summary>
    ///     Seed data
    /// </summary>
    public class CustomersDbSeeder
    {
        private readonly ILogger _Logger;

        public CustomersDbSeeder(ILoggerFactory loggerFactory)
        {
            _Logger = loggerFactory.CreateLogger("CustomersDbSeederLogger");
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            //Based on EF team's example at https://github.com/aspnet/MusicStore/blob/dev/samples/MusicStore/Models/SampleData.cs
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var customersDb = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (await customersDb.Database.EnsureCreatedAsync())
                    if (!await customersDb.Customers.AnyAsync())
                        await InsertCustomersSampleData(customersDb);
            }
        }

        public async Task InsertCustomersSampleData(ApplicationDbContext db)
        {
            var customers = GetCustomers();
            db.Customers.AddRange(customers);

            try
            {
                var numAffected = await db.SaveChangesAsync();
                _Logger.LogInformation($"Saved {numAffected} customers");
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(CustomersDbSeeder)}: " + exp.Message);
                throw;
            }
        }

        private List<Customer> GetCustomers()
        {
            //Customers
            var customerNames = new[]
            {
                "Marcus,HighTower,Male,acmecorp.com",
                "Jesse,Smith,Female,gmail.com",
                "Albert,Einstein,Male,outlook.com",
                "Dan,Wahlin,Male,yahoo.com",
                "Ward,Bell,Male,gmail.com",
                "Brad,Green,Male,gmail.com",
                "Igor,Minar,Male,gmail.com",
                "Miško,Hevery,Male,gmail.com",
                "Michelle,Avery,Female,acmecorp.com",
                "Heedy,Wahlin,Female,hotmail.com",
                "Thomas,Martin,Male,outlook.com",
                "Jean,Martin,Female,outlook.com",
                "Robin,Cleark,Female,acmecorp.com",
                "Juan,Paulo,Male,yahoo.com",
                "Gene,Thomas,Male,gmail.com",
                "Pinal,Dave,Male,gmail.com",
                "Fred,Roberts,Male,outlook.com",
                "Tina,Roberts,Female,outlook.com",
                "Cindy,Jamison,Female,gmail.com",
                "Robyn,Flores,Female,yahoo.com",
                "Jeff,Wahlin,Male,gmail.com",
                "Danny,Wahlin,Male,gmail.com",
                "Elaine,Jones,Female,yahoo.com",
                "John,Papa,Male,gmail.com"
            };
            var addresses = new[]
            {
                "1234 Anywhere St.",
                "435 Main St.",
                "1 Atomic St.",
                "85 Cedar Dr.",
                "12 Ocean View St.",
                "1600 Amphitheatre Parkway",
                "1604 Amphitheatre Parkway",
                "1607 Amphitheatre Parkway",
                "346 Cedar Ave.",
                "4576 Main St.",
                "964 Point St.",
                "98756 Center St.",
                "35632 Richmond Circle Apt B",
                "2352 Angular Way",
                "23566 Directive Pl.",
                "235235 Yaz Blvd.",
                "7656 Crescent St.",
                "76543 Moon Ave.",
                "84533 Hardrock St.",
                "5687534 Jefferson Way",
                "346346 Blue Pl.",
                "23423 Adams St.",
                "633 Main St.",
                "899 Mickey Way"
            };

            var citiesStates = new[]
            {
                "Phoenix,AZ,Arizona",
                "Encinitas,CA,California",
                "Seattle,WA,Washington",
                "Chandler,AZ,Arizona",
                "Dallas,TX,Texas",
                "Orlando,FL,Florida",
                "Carey,NC,North Carolina",
                "Anaheim,CA,California",
                "Dallas,TX,Texas",
                "New York,NY,New York",
                "White Plains,NY,New York",
                "Las Vegas,NV,Nevada",
                "Los Angeles,CA,California",
                "Portland,OR,Oregon",
                "Seattle,WA,Washington",
                "Houston,TX,Texas",
                "Chicago,IL,Illinois",
                "Atlanta,GA,Georgia",
                "Chandler,AZ,Arizona",
                "Buffalo,NY,New York",
                "Albuquerque,AZ,Arizona",
                "Boise,ID,Idaho",
                "Salt Lake City,UT,Utah",
                "Orlando,FL,Florida"
            };

            var citiesIds = new[]
                {5, 9, 44, 5, 36, 17, 16, 9, 36, 14, 14, 6, 9, 24, 44, 36, 25, 19, 5, 14, 5, 23, 38, 17};
            var zip = 85229;

            var orders = new List<Order>
            {
                new Order {Product = "Basket", Price = 29.99M, Quantity = 1},
                new Order {Product = "Yarn", Price = 9.99M, Quantity = 1},
                new Order {Product = "Needes", Price = 5.99M, Quantity = 1},
                new Order {Product = "Speakers", Price = 499.99M, Quantity = 1},
                new Order {Product = "iPod", Price = 399.99M, Quantity = 1},
                new Order {Product = "Table", Price = 329.99M, Quantity = 1},
                new Order {Product = "Chair", Price = 129.99M, Quantity = 4},
                new Order {Product = "Lamp", Price = 89.99M, Quantity = 5},
                new Order {Product = "Call of Duty", Price = 59.99M, Quantity = 1},
                new Order {Product = "Controller", Price = 49.99M, Quantity = 1},
                new Order {Product = "Gears of War", Price = 49.99M, Quantity = 1},
                new Order {Product = "Lego City", Price = 49.99M, Quantity = 1},
                new Order {Product = "Baseball", Price = 9.99M, Quantity = 5},
                new Order {Product = "Bat", Price = 19.99M, Quantity = 1}
            };

            var ordersLength = orders.Count;
            var customers = new List<Customer>();
            var random = new Random();

            for (var i = 0; i < customerNames.Length; i++)
            {
                var nameGenderHost = customerNames[i].Split(',');
                var cityState = citiesStates[i].Split(',');

                var customer = new Customer
                {
                    FirstName = nameGenderHost[0],
                    LastName = nameGenderHost[1],
                    Email = nameGenderHost[0] + '.' + nameGenderHost[1] + '@' + nameGenderHost[3],
                    Address = addresses[i],
                    City = cityState[0],
                    Zip = zip + i
                };

                var firstOrder = (int)Math.Floor(random.NextDouble() * orders.Count);
                var lastOrder = (int)Math.Floor(random.NextDouble() * orders.Count);

                if (firstOrder > lastOrder)
                {
                    var tempOrder = firstOrder;
                    firstOrder = lastOrder;
                    lastOrder = tempOrder;
                }

                customer.Orders = new List<Order>();

                for (var j = firstOrder; j <= lastOrder && j < ordersLength; j++)
                {
                    var order = new Order
                    {
                        Product = orders[j].Product,
                        Price = orders[j].Price,
                        Quantity = orders[j].Quantity
                    };
                    customer.Orders.Add(order);
                }

                customers.Add(customer);
            }

            return customers;
        }
    }
}