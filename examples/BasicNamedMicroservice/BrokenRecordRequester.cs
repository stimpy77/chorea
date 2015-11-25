using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chorea;

namespace BasicNamedMicroservice
{
    public class BrokenRecordRequester : MicroServiceThreadedProcess
    {
        //private IHasMessageQueue<BrokenRecordRequest> _queueSystem;
        private readonly IPublishMessage<BrokenRecordRequest> _messageSystem;


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
                //_queueSystem.MessageQueue.Enqueue(new BrokenRecordRequest { Message = "Marco?" });
                _messageSystem.Publish("Marco", new BrokenRecordRequest {Message="Marco?"});
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
