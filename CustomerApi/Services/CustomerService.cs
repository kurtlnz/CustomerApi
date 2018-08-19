using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _context;

        public CustomerService(CustomerContext customerContext)
        {
            _context = customerContext;

            //Initial customers - For testing
            //if (_context.Customers.Count() == 0)
            //{
            //    _context.Customers.Add(new Customer { FirstName = "John", LastName = "Smith", DateOfBirth = new DateTime(1968, 04, 30) });
            //    _context.Customers.Add(new Customer { FirstName = "Dave", LastName = "Smith", DateOfBirth = new DateTime(1964, 04, 30) });
            //    _context.Customers.Add(new Customer { FirstName = "John", LastName = "Lennon", DateOfBirth = new DateTime(1966, 04, 30) });
            //    _context.SaveChanges();
            //}
        }

        public List<Customer> GetCustomers(CustomerFilterModel filterModel)
        {
            return _context.Customers
                .Where(c => string.IsNullOrWhiteSpace(filterModel.FirstName) || c.FirstName == filterModel.FirstName)
                .Where(c => string.IsNullOrWhiteSpace(filterModel.LastName) || c.LastName == filterModel.LastName)
                .ToList();
        }

        public Customer GetCustomer(long id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public bool UpdateCustomer(long id, Customer customer)
        {
            var cust = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (cust == null)
            {
                return false;
            }

            cust.FirstName = customer.FirstName;
            cust.LastName = customer.LastName;
            cust.DateOfBirth = customer.DateOfBirth;

            _context.Customers.Update(cust);
            _context.SaveChanges();

            return true;
        }

        public bool RemoveCustomer(long id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return true;
        }
    }
}
