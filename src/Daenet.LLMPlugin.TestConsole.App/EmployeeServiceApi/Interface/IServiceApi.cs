using Daenet.LLMPlugin.TestConsole.App.EmployeeServiceApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daenet.LLMPlugin.TestConsole.App.EmployeeServiceApi.Interface
{
    public interface IServiceApi
    {
        Task<EmployeeBookedResult> BookWorkingHour(CustomerDTO customerInfo, DateOnly bookDate, TimeSpan hourToBook, TimeOnly startTime);
        Task<IEnumerable<Location>> GetLocationAsync();
    }
}
