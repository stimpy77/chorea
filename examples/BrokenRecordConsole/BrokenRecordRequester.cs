using System;
using System.Security.AccessControl;
using Chorea;

namespace BrokenRecordConsole
{
    public class BrokenRecordRequester : ThreadedMicroserviceProcess
    {
        Random random = new Random();

        //private IHasMessageQueue<BrokenRecordRequest> _queueSystem;
        readonly IPublishMessage<BrokenRecordRequest> _messageSystem;

        public BrokenRecordRequester(
            //IHasMessageQueue<BrokenRecordRequest> messageTarget)
            IPublishMessage<BrokenRecordRequest> messageTarget)
        {
            //_queueSystem = messageTarget;
            _messageSystem = messageTarget;
        }
        public override void Run()
        {
            while (!Stopped)
            {
                if (random.Next(4) + 1 == 4)
                {
                    Publish("Mifi", "Dupna");
                }
                else
                {
                    Publish("Marco", "Marco?");
                }
                System.Threading.Thread.Sleep(500);
            }
        }

        private void Publish(string recipient, string message)
        {
            //if (recipient == "Marco") _queueSystem.MessageQueue.Enqueue(new BrokenRecordRequest { Message = message });
            _messageSystem.Publish(recipient, new BrokenRecordRequest { Message = message });
            if (recipient != "Marco") Console.WriteLine("This will be ignored: " 
                + message + " (for: " + recipient + ")");
        }
    }
}
