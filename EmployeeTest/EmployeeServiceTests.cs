using EmployeeTest.Services;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
<<<<<<< Updated upstream


=======
using System.Net.Http.Json;
using NUnit.Framework;
using EmployeeManagement;
using Employee = EmployeeTest.Services.Employee;
using System.Threading.Tasks;
>>>>>>> Stashed changes

namespace EmployeeTest
{
    public class Tests
    {
        private EmployeeService _employeeService;
        private Mock<IRestApiClient> restApiClientMock;

        [SetUp]
        public void Setup()
        {
<<<<<<< Updated upstream
            var restApiClientMock = new Mock<IRestApiClient>();
            var employeeService = new EmployeeService();
           // employeeService = new EmployeeService(restApiClientMock.Object);
        }
=======
            _employeeService = new EmployeeService();            
        }            
             
>>>>>>> Stashed changes
        public class TestHttpClientHandler : HttpMessageHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                // Implement the desired behavior for your test case
                // You can create and return a mock HttpResponseMessage based on the request

                // Example: Return a success response with status code 200
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                return await Task.FromResult(response);
            }
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void Test_GetEmployees_ReturnsListOfEmployees()
        {
            EmployeeService employeeService = new EmployeeService();
            var employees = employeeService.GetEmployees().Result;
            Assert.IsNotNull(employees);
            if (employees.Count > 0)
            {
                Assert.IsTrue(employees.Count > 0);
            }
        }
        
        [Test]
        public async Task UpdateEmployee_InvalidData_ReturnsNull()
        {
            // Arrange
<<<<<<< Updated upstream
            var employeeToUpdate = new Employee
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            var updatedEmployee = new Employee
            {
                Id = 1,
                Name = "John Smith",
                Email = "john.smith@example.com"
            };

            EmployeeService employeeService = new EmployeeService();            
            var employees = employeeService.GetEmployees().Result;
            Assert.IsNotNull(employees);
            if (employees.Count > 0)
            {
                Assert.IsTrue(employees.Count > 0);
            }
        }
=======
            
            var invalidEmployee = 1; // Create an invalid employee object
            
            // Act
            var updatedEmployee = await _employeeService.UpdateEmployee(invalidEmployee);                      
>>>>>>> Stashed changes

            // Assert
            Assert.IsFalse(updatedEmployee);
        }
        
        [Test]
        public async Task DeleteEmployee_NotFound_ReturnsFalse()
        {
<<<<<<< Updated upstream
            EmployeeService employeeService = new EmployeeService();
            var employees = employeeService.GetEmployees().Result;
            Assert.IsNotNull(employees);
            if (employees.Count > 0)
            {
                Assert.IsTrue(employees.Count > 0);
            }
=======
            // Arrange
            int employeeId = 1; 

            // Act
            var result = await _employeeService.DeleteEmployee(employeeId);

            // Assert
            Assert.IsFalse(result);            
>>>>>>> Stashed changes
        }
    }
}
