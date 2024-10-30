using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daenet.LLMPlugin.TestConsole.App.EmployeeServiceApi.Entities
{
    public class EmployeeBookedResult
    {
        public string CustomerName { get; set; }
        public string OrderName { get; set; }
        public string ActivityId { get; set; }
        public DateTime StartWorkingTime { get; set; }
        public DateTime StopWorkingTime { get; set; }
        public string Warning { get; set; }
    }
}
