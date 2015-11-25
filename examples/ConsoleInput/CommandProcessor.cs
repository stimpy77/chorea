using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chorea;

namespace ConsoleInput
{
    public class CommandProcessor
    {
        readonly ThreadedMessageEventDispatcher<UserCommandMessage> microservices;

        public CommandProcessor(ThreadedMessageEventDispatcher<UserCommandMessage> microservices)
        {
            this.microservices = microservices;
            microservices.MessageReceived += OnMessageReceived;
        }

        protected void OnMessageReceived(object sender, MessageEventArgs<UserCommandMessage> eventArgs)
        {
            var message = eventArgs.Message.Value as QueueMessage;
            if (message == null) return;
            switch (message.MessageType)
            {
                case "UserCommand":
                    ExecuteCommand(message.Message);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Error: Unknown message type: " + message.MessageType);
                    Console.ResetColor();
                    break;
            }
        }

        public void ExecuteCommand(string command)
        {
            var cmd = command.Split(' ')[0];
            switch (cmd)
            {
                case "exit":
                    microservices.Stop();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("I don't understand command: " + command);
                    Console.WriteLine("Try \"exit\".");
                    Console.ResetColor();
                    break;
            }
        }
    }
}
