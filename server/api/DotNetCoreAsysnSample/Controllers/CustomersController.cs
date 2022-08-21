using DotNetCoreAsysnSample.Models;
using DotNetCoreAsysnSample.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreAsysnSample.Controllers
{
    [Route("api/v1/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    public class CustomersController : Controller
    {
        private readonly ICustomersRepositoryAsync _customersRepository;
        private readonly ILogger _logger;

        public CustomersController(ICustomersRepositoryAsync customersRepo, ILoggerFactory loggerFactory)
        {
            _customersRepository = customersRepo;
            _logger = loggerFactory.CreateLogger(nameof(CustomersController));
        }

        /// <summary>
        ///     Get all customers
        ///     Route : GET : api/customers
        /// </summary>
        /// <response code="200">Customers returned</response>
        /// <response code="400">Customer has missing/invalid values</response>
        /// <response code="500">Oops! Can't create your product right now</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Customers()
        {
            try
            {
                var customers = await _customersRepository.GetCustomersAsync();
                return Ok(customers);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false, Error = exp.Message });
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
        [ProducesResponseType(typeof(List<Customer>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CustomersPage(int skip, int take)
        {
            try
            {
                var pagingResult = await _customersRepository.GetCustomersPageAsync(skip, take);
                Response.Headers.Add("X-InlineCount", pagingResult.TotalRecords.ToString());
                return Ok(pagingResult.Records);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false, Error = exp.Message });
            }
        }

        /// <summary>
        ///     Get customer by id
        ///     Route : GET : api/customers/1
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer</returns>
        [HttpGet("{id}", Name = "GetCustomerRoute")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Customers(int id)
        {
            try
            {
                var customer = await _customersRepository.GetCustomerAsync(id);
                return Ok(customer);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false, Error = exp.Message });
            }
        }

        /// <summary>
        ///     Create new customer
        ///     Route : POST : api/customers
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>New customer and Route</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });

            try
            {
                var newCustomer = await _customersRepository.InsertCustomerAsync(customer);
                if (newCustomer == null) return BadRequest(new ApiResponse { Status = false });
                return CreatedAtRoute("GetCustomerRoute", new { id = newCustomer.Id },
                    newCustomer);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        /// <summary>
        ///     Update customer
        ///     Route : PUT : api/customers/1
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="customer">Customer</param>
        /// <returns>Updated Customer</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });

            try
            {
                var status = await _customersRepository.UpdateCustomerAsync(customer);
                if (!status) return BadRequest(new ApiResponse { Status = false });
                return Ok(customer);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        /// <summary>
        ///     Delete Customer
        ///     Route : PUT : api/customers/1
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>APIResponse</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                var status = await _customersRepository.DeleteCustomerAsync(id);
                if (!status) return BadRequest(new ApiResponse { Status = false });
                return Ok(new ApiResponse { Status = true });
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}