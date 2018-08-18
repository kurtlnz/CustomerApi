using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CustomerApi.Tests
{
    [TestClass]
    public class TestCustomerService
    {
        //[TestMethod]
        //public void GetAllCustomers_ShouldReturnAllCustomers()
        //{

        //}

        //[TestMethod]
        //public void GetCustomer_ShouldReturnACustomer()
        //{
        //}

        [TestMethod]
        public void AddCustomer_ShouldAddACustomer()
        {
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: "AddCustomer_Test")
                .Options;

            // Run the test against one instance of the context
            using (var context = new CustomerContext(options))
            {
                var service = new BlogService(context);
                service.Add("http://sample.com", "John", "05/10/19");
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CustomerContext(options))
            {
                Assert.AreEqual(1, context.Customers.Count());
                Assert.AreEqual("http://sample.com", context.Customers.Single().FirstName);
            }
        }

        //[TestMethod]
        //public void UpdateCustomer_ShouldUpdateACustomer()
        //{
        //}

        //[TestMethod]
        //public void RemoveCustomer_ShouldRemoveACustomer()
        //{
        //}
    }
}
