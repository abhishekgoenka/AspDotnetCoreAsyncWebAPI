using System;
using System.Threading.Tasks;
using DotNetCoreAsysnSample.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCoreAsysnSample.Controllers
{
    [Route("api/customers")]
    [EnableCors("AllowAnyOrigin")]
    public class CustomersApiControllerAsync : Controller
    {
        private readonly ICustomersRepositoryAsync _CustomersRepository;
        private readonly ILogger _Logger;

        public CustomersApiControllerAsync(ICustomersRepositoryAsync customersRepo, ILoggerFactory loggerFactory)
        {
            _CustomersRepository = customersRepo;
            _Logger = loggerFactory.CreateLogger(nameof(CustomersApiControllerAsync));
        }

        /// <summary>
        ///     Get all customers
        ///     Route : GET : api/customers
        /// </summary>
        /// <returns>Customers</returns>
        [HttpGet]
        public async Task<ActionResult> Customers()
        {
            try
            {
                var customers = await _CustomersRepository.GetCustomersAsync();
                return Ok(customers);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new {Status = false});
            }
        }

        /// <summary>
        ///     Get customers by page
        ///     Route : GET : api/customers/page/1/10
        /// </summary>
        /// <param name="skip">Pages to skip</param>
        /// <param name="take">Number of records to return</param>
        /// <returns>Customers of selected page</returns>
        [HttpGet("page/{skip}/{take}")]
        public async Task<ActionResult> CustomersPage(int skip, int take)
        {
            try
            {
                var pagingResult = await _CustomersRepository.GetCustomersPageAsync(skip, take);
                Response.Headers.Add("X-InlineCount", pagingResult.TotalRecords.ToString());
                return Ok(pagingResult.Records);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new {Status = false});
            }
        }

        /// <summary>
        ///     Get customer by id
        ///     Route : GET : api/customers/1
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Customers(int id)
        {
            try
            {
                var customer = await _CustomersRepository.GetCustomerAsync(id);
                return Ok(customer);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new {Status = false});
            }
        }
    }
}