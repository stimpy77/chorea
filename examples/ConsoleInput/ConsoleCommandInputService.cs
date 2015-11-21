using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Chorea;

namespace ConsoleInput
{
    public class ConsoleCommandInputService : MicroServiceThreadedProcess,
        IHasMessageQueue
    //IHasPublishedMessages 
    {
        //public IPublishedMessages PublishedMessages { get; set; } = new LocalMessagePublishContainer();

        readonly BasicMessageQueueContainer _messageQueue = new BasicMessageQueueContainer();
        public Queue<object> MessageQueue => _messageQueue.MessageQueue;

        public override void Run()
        {
            while (!Stopped)
            {
                var command = Console.ReadLine();
                if (!string.IsNullOrEmpty((command ?? "").Trim()))
                {
                    _messageQueue.EnqueueMessage(new UserCommandMessage(command));
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
