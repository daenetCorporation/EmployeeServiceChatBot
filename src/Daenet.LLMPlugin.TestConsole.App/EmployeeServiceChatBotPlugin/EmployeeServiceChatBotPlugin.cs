using Daenet.LLMPlugin.TestConsole.App.EmployeeServiceApi.Interface;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Text;

namespace Daenet.LLMPlugin.TestConsole.App.EmployeeServiceChatBotPlugin
{
    public class EmployeeServiceChatBotPlugin
    {
        private readonly EmployeeServiceChatBotPluginConfig _cfg;
        private readonly IServiceApi _serviceApi;

        public EmployeeServiceChatBotPlugin(EmployeeServiceChatBotPluginConfig cfg, IServiceApi serviceApi)
        {
            _cfg = cfg;
            _serviceApi = serviceApi;
        }

        [KernelFunction]
        [Description("Returns a list of locations")]
        public string GetLocations()
        {
            var locations = _serviceApi.GetLocationAsync().Result;
            string filt = "Do not list all locations, if user filtered but no result found. Locations: ";

            return filt + String.Join(',', locations.Select(d => d.Name).ToList());
        }

        #region Project Management

        /// <summary>
        /// Book the working hour for the project
        /// </summary>
        /// <param name="projectName">name of the project</param>
        /// <param name="hours">number of working hours</param>
        /// <param name="date">the date for booking</param>
        /// <returns>string content notify about the booked hours</returns>
        [KernelFunction]
        [Description("Book working hours")]
        public async Task<string> BookWorkingHoursWithHoursAsync([Description("Project name.")] string projectName, [Description("hours to be booked")] int hours, [Description("date when to book the hours")] DateTime date)
        {
            /*
             * here we book the working hours for the project base on the project name and the number of hours
             */

            var customers = await serviceApi.GetCustomersAsync();

           var project = customers.SelectMany(c => c.Orders).ToList().FirstOrDefault(o => o.OrderName == projectName);

            if (project == null)
            {
                return $"The project {projectName} is not found.";
            }


            return $"you booked {hours} hour in the project {projectName} on date {date}";
        }


        //[KernelFunction]
        //[Description("Book the working hour for the project")]
        //public string BookWorkingHoursWithDateTime([Description("project name")] string projectName, [Description("starting datetime")] DateTime start, [Description("ending datetime")] DateTime end)
        //{
        //    /*
        //     * here we book the working hours for the project base on the project name and the number of hours
        //     */

        //    return $"The working hours from {start} to {end} for the project {projectName} are booked successfully.";
        //}

        /// <summary>
        /// Get the deadline/ending date of the project
        /// </summary>
        /// <param name="projectName">name of the project</param>
        /// <returns></returns>
        [KernelFunction]
        [Description("Get the deadline/ending date of the project")]
        public string GetProjectDeadline([Description("The project name")] string projectName)
        {
            /*
             * here we get the deadline of the project base on the project name 
             */

            return $"The deadline of the project {projectName} is 12/31/2022.";
        }


        #endregion

        [KernelFunction]
        [Description("Provides the list of customers")]
        public string GetProjectInfo([Description("If set in true, it provides the list of orders for each customer")] bool provideOrderInfo = false)
        {
            StringBuilder sb = new StringBuilder();

            var customers = _serviceApi.GetCustomersAsync().Result;

            foreach (var customer in customers)
            {
                sb.AppendLine($"Customer name: {customer.CustomerName}");

                if (provideOrderInfo)
                {
                    foreach (var order in customer.Orders)
                    {
                        sb.AppendLine($"Order name: {order.OrderName}");
                        if (order.Activities == null)
                            continue;
                        sb.AppendLine($"Activities: {string.Join(", ", order.Activities)}");
                    }
                }
            }
            return sb.ToString();
        }
    }
}
