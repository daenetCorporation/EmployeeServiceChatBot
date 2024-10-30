using Daenet.LLMPlugin.TestConsole.App.EmployeeServiceApi.Entities;
using Daenet.LLMPlugin.TestConsole.App.EmployeeServiceApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daenet.LLMPlugin.TestConsole.App.EmployeeServiceApi
{
    public class ServiceApiMock : IServiceApi
    {
        public static IEnumerable<Location> Locations = new List<Location>()
        {
            new() {Id = 1, Name = "Berlin"},
            new() {Id = 2, Name = "München"},
            new() {Id = 3, Name = "Frankfurt"},
            new() {Id = 3, Name = "Zagreb"}
        };

        public async Task<IEnumerable<Location>> GetLocationAsync()
        {
            return Locations;
        }

        private static IEnumerable<Customer> _customers = new List<Customer>()
        {
            new()
            {
                CustomerName = "Test",
                Orders =
                [
                    new(){OrderName = "Test-Order1", Activities = ["Test-Order1-Activity1"]},
                    new(){OrderName = "Test-Order2", Activities = ["Test-Order2-Activity2"]}
                ]
            },
            new()
            {
                CustomerName = "Fraenkische",
                Orders =
                [
                    new(){OrderName = "Test-Order1"}
                ]
            },
            new()
            {
                CustomerName = "Daenet",
                Orders =
                [
                    new(){OrderName = "EmployeeService"}
                ]
            }
        };


        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return _customers;
        }

        public async Task<EmployeeBookedResult> BookWorkingHour(CustomerDTO customerInfo, DateOnly bookDate, TimeSpan hourToBook, TimeOnly startTime)
        {
            var customerInDb = _customers.FirstOrDefault(c => c.CustomerName == customerInfo.CustomerName);
            List<string> warnings = new List<string>();
            EmployeeBookedResult result = new EmployeeBookedResult();
            result.CustomerName = customerInfo.CustomerName;
            if (customerInDb == null)
            {
                throw new Exception("CustomerNotAvailable");
            }

            if (!string.IsNullOrWhiteSpace(customerInfo.OrderName))
            {
                var orderOfCustomer = customerInDb.Orders.FirstOrDefault(o => o.OrderName == customerInfo.OrderName);
                if (orderOfCustomer == null)
                {
                    warnings.Add($"User want to book for order {customerInfo.OrderName} but it is not exist for customer {customerInfo.CustomerName}");
                }
                else
                {
                    result.OrderName = orderOfCustomer.OrderName;
                    if (string.IsNullOrWhiteSpace(customerInfo.ActivityId))
                    {
                        var activity = orderOfCustomer.Activities.FirstOrDefault();
                        if (activity == null)
                        {
                            warnings.Add($"User want to book activity {customerInfo.ActivityId} but it is not exist for order {customerInfo.OrderName}");
                        }
                        else
                        {
                            result.ActivityId = customerInfo.ActivityId;
                        }

                    }
                }
            }

            DateTime startWorkingTime = bookDate.ToDateTime(startTime);
            DateTime stopWorkingTime = bookDate.ToDateTime(startTime).AddTicks(hourToBook.Ticks);

            result.StartWorkingTime = startWorkingTime;
            result.StopWorkingTime = stopWorkingTime;

            result.Warning = string.Join("; ", warnings);

            return result;
        }
    }
}
