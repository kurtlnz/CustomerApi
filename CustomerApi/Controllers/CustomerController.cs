using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CustomerApi.Models;
using System;
using CustomerApi.Service;

namespace CustomerApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            /* Inject ICustomerService interface into constructor 
             * and throw exception if customerService is null. */
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAll()
        {
            return _customerService.GetAllCustomers();
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public ActionResult<Customer> GetById(long id)
        {
            var customer = _customerService.GetCustomer(id);

            // Return 404 if cannot find customer with id
            if(customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            // Add Customer
            _customerService.AddCustomer(customer);

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Customer cust)
        {
            // Try update Customer
            var success = _customerService.UpdateCustomer(id, cust);

            // Return 404 if cannot find customer with id
            if (!success)
            {
                return NotFound();
            }

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            // Try remove Customer
            var success = _customerService.RemoveCustomer(id);

            // Return 404 if cannot find customer with id
            if (!success)
            {
                return NotFound();
            }

            return Ok();

        }
    }
}