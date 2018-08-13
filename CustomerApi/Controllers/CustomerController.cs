using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CustomerApi.Models;
using System;

namespace TodoApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;

            if (_context.Customers.Count() == 0)
            {
                _context.Customers.Add(new Customer { FirstName = "John", LastName = "Smith", DateOfBirth = new DateTime(1964, 04, 30) });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAll()
        {
            return _context.Customers.ToList();
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public ActionResult<Customer> GetById(long id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Customer cust)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.FirstName = cust.FirstName;
            customer.LastName = cust.LastName;
            customer.DateOfBirth = cust.DateOfBirth;

            _context.Customers.Update(customer);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}