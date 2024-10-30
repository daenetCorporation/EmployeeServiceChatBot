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
            var locations =  serviceApi.GetLocationAsync().Result;
            return locations.Where(l => string.IsNullOrEmpty(locationFilter) || l.Name.Contains(locationFilter)).Select(d => d.Name).Aggregate((a, b) => a + Environment.NewLine + b);
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
