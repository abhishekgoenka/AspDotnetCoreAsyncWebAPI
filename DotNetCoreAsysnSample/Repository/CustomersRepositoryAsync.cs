﻿using DotNetCoreAsysnSample.Models;
using DotNetCoreAsysnSample.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAsysnSample.Repository
{
    public class CustomersRepositoryAsync : ICustomersRepositoryAsync
    {
        private readonly CustomersDbContext _Context;
        private readonly ILogger _Logger;

        public CustomersRepositoryAsync(CustomersDbContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("CustomersRepository");
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _Context.Customers.OrderBy(c => c.LastName)
                                 .ToListAsync();
        }

        public async Task<PagingResult<Customer>> GetCustomersPageAsync(int skip, int take)
        {
            var totalRecords = await _Context.Customers.CountAsync();
            var customers = await _Context.Customers
                                 .OrderBy(c => c.LastName)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToListAsync();
            return new PagingResult<Customer>(customers, totalRecords);
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _Context.Customers
                                 .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
