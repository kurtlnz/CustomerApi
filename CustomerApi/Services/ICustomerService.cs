using CustomerApi.Models;
using System.Collections.Generic;

namespace CustomerApi.Service
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomer(long id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(long id, Customer customer);
        void RemoveCustomer(long id);
    }
}
