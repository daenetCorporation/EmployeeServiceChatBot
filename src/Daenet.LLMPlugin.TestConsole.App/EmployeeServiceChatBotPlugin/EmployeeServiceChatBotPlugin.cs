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
        public string GetLocations([Description("If provided, the locations returned are filtered.")] string locationFilter = null)
        {
            var locations = serviceApi.GetLocationAsync().Result;
            return locations.Where(l => string.IsNullOrEmpty(locationFilter) || l.Name.Contains(locationFilter)).Select(d => d.Name).Aggregate((a, b) => a + Environment.NewLine + b);
        }

        #region Project Management

        [KernelFunction]
        [Description("Book the working for the project using hours and date")]
        public string BookWorkingHoursWithHours([Description("The project name")] string projectName, [Description("hours")] int hours, [Description("date")] DateOnly date)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                return "the project name is missing please provide it";
            }

            if (hours <= 0)
            {
                return "please provide a valid number of hours";
            }
            /*
             * here we book the working hours for the project base on the project name and the number of hours
             */

            return $"you booked {hours} hour in the project {projectName} on date {date}";
        }


        [KernelFunction]
        [Description("Book the working hour for the project")]
        public string BookWorkingHoursWithDateTime([Description("project name")] string projectName, [Description("starting datetime")] DateTime start, [Description("ending datetime")] DateTime end)
        {
            /*
             * here we book the working hours for the project base on the project name and the number of hours
             */

            return $"The working hours from {start} to {end} for the project {projectName} are booked successfully.";
        }


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
