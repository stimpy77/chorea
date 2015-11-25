using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Chorea;

namespace ConsoleInput
{
    public class ConsoleCommandInputService : MicroServiceThreadedProcess,
        IHasMessageQueue<QueueMessage>
        //IHasPublishedMessages 
    {
        //public IPublishedMessages PublishedMessages { get; set; } = new LocalMessagePublishContainer();

        readonly BasicMessageQueueContainer<QueueMessage> _messageQueue = new BasicMessageQueueContainer<QueueMessage>();
        public ConcurrentQueue<QueueMessage> MessageQueue => _messageQueue.MessageQueue;
        public string QueueName => "CommandInput";
        private string Prompt { get; set; } = "? ";

        public override void Run()
        {
            Console.WriteLine("Enter a command.");
            while (!Stopped)
            {
                Console.Write(Prompt);
                var command = Console.ReadLine();
                if (!string.IsNullOrEmpty((command ?? "").Trim()))
                {
                    _messageQueue.EnqueueMessage("UserCommand", new UserCommandMessage(command));
                    //((LocalMessagePublishContainer)PublishedMessages).Publish(new UserCommandMessage(command));
                }
            }
        }



        const int VK_RETURN = 0x0D;
        const int WM_KEYDOWN = 0x100;

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        public ConsoleCommandInputService()
        {
            Stopping += (o, args) =>
            {
                // abort readline
                var hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                PostMessage(hWnd, WM_KEYDOWN, VK_RETURN, 0);
            };
        }
    }
}
