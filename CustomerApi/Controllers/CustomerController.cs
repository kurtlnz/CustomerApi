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

            if(customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _customerService.AddCustomer(customer);

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Customer cust)
        {
            _customerService.UpdateCustomer(id, cust);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _customerService.RemoveCustomer(id);

            return NoContent();
        }
    }
}