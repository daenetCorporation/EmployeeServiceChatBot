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

        public EmployeeServiceChatBotPlugin(EmployeeServiceChatBotPluginConfig cfg)
        {
            _cfg = cfg;
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
