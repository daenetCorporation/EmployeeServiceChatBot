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

        //private readonly EmployeeService _service;

        public EmployeeServiceChatBotPlugin(EmployeeServiceChatBotPluginConfig cfg)
        {
            _cfg = cfg;
            //_service = new EmployeeService("", new HttpClient());
        }


        #region Project kernel functions
        [KernelFunction]
        [Description("Provide the remaining hours for the project of a customer")]
        public string GetProjectRemainingHours([Description("The customer name")] string customerName, [Description("The project name")] string projectName)
        {

            if (string.IsNullOrEmpty(customerName))
            {
                return "Please provide the customerName";
            }

            if (string.IsNullOrEmpty(projectName))
            {
                return "Please provide the projectName";
            }

            /*
             * here we get the remaining hours base on the project name 
             */

            return "Remaining hours for the project are 100.";
        }

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
