using Azure;
using Daenet.LLMPlugin.TestConsole.App.EmployeeServiceApi.Interface;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Daenet.LLMPlugin.TestConsole.App.EmployeeServiceChatBotPlugin
{
    public class EmployeeServiceChatBotPlugin
    {
        private readonly EmployeeServiceChatBotPluginConfig _cfg;
        private readonly IServiceApi serviceApi;

        public EmployeeServiceChatBotPlugin(EmployeeServiceChatBotPluginConfig cfg, IServiceApi serviceApi)
        {

            _cfg = cfg;
            this.serviceApi = serviceApi;
        }

        [KernelFunction]
        [Description("Returns a list of locations")]
        public string GetLocations()
        {
            var locations = serviceApi.GetLocationAsync().Result;
            string filt = "Do not list all locations, if user filtered but no result found. Locations: ";

            return filt + String.Join(',', locations.Select(d => d.Name).ToList());
        }

        #region Project Management

        [KernelFunction]
        [Description("Book working hours")]
        public string BookWorkingHoursWithHours([Description("Project name.")] string projectName, [Description("hours to be booked")] int hours, [Description("date when to book the hours")] DateTime date)
        {

            if (hours <= 0)
            {
                return "please provide a valid number of hours";
            }
            /*
             * here we book the working hours for the project base on the project name and the number of hours
             */

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


        [KernelFunction]
        [Description("Get the deadline/ending date of the project")]
        public string GetProjectDeadline([Description("The project name")] string projectName)
        {
            /*
             * here we get the deadline of the project base on the project name 
             */

            return "The deadline of the project is 12/12/2022.";
        }


        #endregion

        [KernelFunction]
        [Description("Provides the list of customers")]
        public string GetProjectInfo([Description("If set in true, it provides the list of orders for each customer")] bool provideOrderInfo = false)
        {
            StringBuilder sb = new StringBuilder();

            var customers = serviceApi.GetCustomersAsync().Result;

            foreach (var customer in customers)
            {
                sb.AppendLine($"Customer name: {customer.CustomerName}");

                if(provideOrderInfo)
                {
                    foreach (var order in customer.Orders)
                    {
                        sb.AppendLine($"Order name: {order.OrderName}");
                        //sb.AppendLine($"Activities: {string.Join(", ", order.Activities)}");
                    }
                }
            }
            return sb.ToString();
        }

        [KernelFunction]
        [Description("Provides the list of names of processes")]
        public string GetProcessInfo([Description("If set in true, it provides the detaile process information.")] bool provideDetailedInfo = false)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Process proc in Process.GetProcesses().ToList())
            {
                sb.AppendLine($"Proce name: {proc.ProcessName}");
                if (provideDetailedInfo)
                {
                    sb.AppendLine($"Proces ID: {proc.Id}"); ;
                    sb.AppendLine($"Proces working set: {proc.WorkingSet64}");
                    sb.AppendLine("---------------------------------------------");
                }
            }

            return sb.ToString();
        }

    }
}
