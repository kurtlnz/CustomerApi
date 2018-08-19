
# CustomerApi 

By Kurt Lim
https://github.com/lim-code/
klim049@gmail.com


## Coding challenge for Acurus 
Goal is to implement a RESTful web service that performs CRUD operations:
- Using C# and .NET Core Web API framework
- Including automated unit tests
- Using in-memory persistence

### Extensions
In this project I have included:
- A lightweight UI in the form of Swagger UI (https://swagger.io/)
- Filtering by FirstName and LastName on the GetAll Api call

# Getting Started
To get this program you will need to clone the CustomerApi repository from GitHub to your local computer.

You can follow this guide to this here: https://help.github.com/articles/cloning-a-repository/

Clone URL: https://github.com/lim-code/CustomerApi.git

The projects in this solution also target .NET Core 2.1. Note: to use .NET Core 2.1 with Visual Studio, you'll need Visual Studio 2017 15.7 or newer.

Download the .NET Core 2.1 SDK here:
https://www.microsoft.com/net/download/dotnet-core/2.1

Download Visual Studio
https://visualstudio.microsoft.com/downloads/


## Installing and Running

Luckily once the prerequisites are installed there is little else to install!

Open the solution and ensure all the solution builds correctly.

Run the program in your IDE. If the application does not open up the Swagger UI by default navigate to:

localhost:{portNumber}/swagger


## Running the tests

This solution includes some basic unit tests for the service layer (CustomerService.cs).

This includes testing CRUD operations and filtering.

To isolate the test data, I have each test invoking a unique in-memory database context to ensure they are only testing their own functionality.

To run these tests in Microsoft Visual Studio:
- Open the CustomerApi solution
- In the Test menu
- Run > All Tests
