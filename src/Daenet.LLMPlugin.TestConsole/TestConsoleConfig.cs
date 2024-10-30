using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daenet.LLMPlugin.TestConsole
{
    public class TestConsoleConfig
    {
        /// <summary>
        /// The color of system prompt.
        /// </summary>
        public ConsoleColor PromptColor { get; set; } = ConsoleColor.Green;

        /// <summary>
        /// The color of the user input.
        /// </summary>
        public ConsoleColor UserInputColor { get; set; } = ConsoleColor.Yellow;

        /// <summary>
        /// The system prompt.
        /// </summary>
        public string SystemPrompt { get; set; } = "User > ";

        /// <summary>
        /// The assistent output color.
        /// </summary>
        public ConsoleColor AssistentMessageColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// The system message used to initialize the conversation..
        /// </summary>
        public string SystemMessage { get; set; } = "";
    }
}
