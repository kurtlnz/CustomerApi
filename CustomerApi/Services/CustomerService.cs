using CustomerApi.Models;
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

            // Initial customer
            if (_context.Customers.Count() == 0)
            {
                _context.Customers.Add(new Customer { FirstName = "John", LastName = "Smith", DateOfBirth = new DateTime(1964, 04, 30) });
                _context.SaveChanges();
            }
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
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

        public void UpdateCustomer(long id, Customer customer)
        {
            var cust = _context.Customers.Find(id);

            if (cust == null)
            {
                throw new ArgumentNullException("customer");
            }

            cust.FirstName = customer.FirstName;
            cust.LastName = customer.LastName;
            cust.DateOfBirth = customer.DateOfBirth;

            _context.Customers.Update(cust);
            _context.SaveChanges();
        }

        public void RemoveCustomer(long id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
            {
                throw new ArgumentNullException("id");
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
