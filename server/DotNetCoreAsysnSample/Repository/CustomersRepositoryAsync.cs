﻿using DotNetCoreAsysnSample.Models;
using DotNetCoreAsysnSample.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAsysnSample.Repository
{
    public class CustomersRepositoryAsync : ICustomersRepositoryAsync
    {
        private readonly ApplicationDbContext _Context;
        private readonly ILogger _Logger;

        public CustomersRepositoryAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
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

        public async Task<Customer> InsertCustomerAsync(Customer customer)
        {
            _Context.Add(customer);
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(InsertCustomerAsync)}: " + exp.Message);
            }

            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            //Will update all properties of the Customer
            _Context.Customers.Attach(customer);
            _Context.Entry(customer).State = EntityState.Modified;
            try
            {
                return await _Context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(UpdateCustomerAsync)}: " + exp.Message);
            }

            return false;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            //Extra hop to the database but keeps it nice and simple for this demo
            //Including orders since there's a foreign-key constraint and we need
            //to remove the orders in addition to the customer
            var customer = await _Context.Customers
                .Include(c => c.Orders)
                .SingleOrDefaultAsync(c => c.Id == id);
            _Context.Remove(customer);
            try
            {
                return await _Context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteCustomerAsync)}: " + exp.Message);
            }

            return false;
        }
    }
}