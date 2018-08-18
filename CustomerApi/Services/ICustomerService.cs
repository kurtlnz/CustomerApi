using CustomerApi.Models;
using System.Collections.Generic;

namespace CustomerApi.Service
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomer(long id);
        void AddCustomer(Customer customer);
        bool UpdateCustomer(long id, Customer customer);
        bool RemoveCustomer(long id);
    }
}
