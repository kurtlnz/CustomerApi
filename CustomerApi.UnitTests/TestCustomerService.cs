using CustomerApi.Models;
using CustomerApi.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApi.Tests.MSTest
{
    [TestClass]
    public class TestCustomerService
    {
        /* I have implemented one basic unit test for each method
         * in the service layer. Typically you want a clean database 
         * for testing each method so in the 'Arrange' sections
         * in my tests I've used the below method to generate 
         * unique contexts for each test. */

        // Create DB Context for Customer Unit Tests
        private CustomerContext CreateDummyContext(string dbName)
        {
            // Create DbContextOptions
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create CustomerContext with temp db name
            var context = new CustomerContext(options);

            return context;
        }

        // Generate dummy data for Customer Unit Tests if needed
        private void GenerateDummyCustomers(CustomerContext context)
        {
            context.Add(new Customer { Id = 1, FirstName = "Kurt", LastName = "Cobain", DateOfBirth = DateTime.Today });
            context.Add(new Customer { Id = 2, FirstName = "Dave", LastName = "Grohl", DateOfBirth = DateTime.Today.AddDays(1) });
            context.Add(new Customer { Id = 3, FirstName = "Jim", LastName = "Morrison", DateOfBirth = DateTime.Today.AddDays(2) });
            context.SaveChanges();
        }

        [TestMethod]
        public void GetCustomers_ShouldReturnAllCustomers()
        {
            // Arrange
            var context = CreateDummyContext("GetCustomers_Test");
            var service = new CustomerService(context);
            GenerateDummyCustomers(context);

            // Act
            var result = service.GetCustomers(new CustomerFilterModel { }) as List<Customer>;

            //Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetCustomers_FilterBy_ShouldReturnFilteredCustomers()
        {
            // Arrange
            var context = CreateDummyContext("GetCustomersFiltered_Test");
            var service = new CustomerService(context);
            GenerateDummyCustomers(context);

            // Act
            var result = service.GetCustomers(new CustomerFilterModel { FirstName = "Kurt" }) as List<Customer>;

            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetCustomer_ShouldReturnCorrectCustomer()
        {
            // Arrange
            var context = CreateDummyContext("GetCustomer_Test");
            var service = new CustomerService(context);
            GenerateDummyCustomers(context);

            // Act
            var customer = service.GetCustomer(2);

            //Assert
            Assert.AreEqual("Dave", customer.FirstName);
            Assert.AreEqual("Grohl", customer.LastName);
            Assert.AreEqual(DateTime.Today.AddDays(1), customer.DateOfBirth);
        }

        [TestMethod]
        public void AddCustomer_ShouldAddACustomer()
        {
            // Arrange
            var context = CreateDummyContext("AddCustomer_Test");
            var service = new CustomerService(context);

            // Act
            service.AddCustomer(new Customer { FirstName = "Kurt", LastName = "Cobain", DateOfBirth = DateTime.Today });

            // Assert
            Assert.AreEqual(1, context.Customers.Count());
        }

        [TestMethod]
        public void UpdateCustomer_ShouldUpdateACustomer()
        {
            // Arrange
            var context = CreateDummyContext("UpdateCustomer_Test");
            var service = new CustomerService(context);
            GenerateDummyCustomers(context);

            // Act
            var customer = new Customer { FirstName = "Freddie", LastName = "Mercury", DateOfBirth = DateTime.Today.AddDays(2) };
            service.UpdateCustomer(2, customer);

            //Assert
            var result = context.Customers.Single(c => c.Id == 2);

            Assert.AreEqual("Freddie", result.FirstName);
            Assert.AreEqual("Mercury", result.LastName);
            Assert.AreEqual(DateTime.Today.AddDays(2), result.DateOfBirth);
        }

        [TestMethod]
        public void RemoveCustomer_ShouldRemoveACustomer()
        {
            // Arrange
            var context = CreateDummyContext("RemoveCustomer_Test");
            var service = new CustomerService(context);

            var customer = new Customer { FirstName = "Kurt", LastName = "Cobain", DateOfBirth = DateTime.Today };
            context.Add(customer);
            context.SaveChanges();

            // Act
            service.RemoveCustomer(customer.Id);

            // Assert
            Assert.AreEqual(0, context.Customers.Count());
        }

    }
}