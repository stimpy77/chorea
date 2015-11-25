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
        private IHasMessageQueue<BrokenRecordRequest> _queueSystem;
        //private readonly IPublishMessage _messageSystem;


        public BrokenRecordRequester(IHasMessageQueue<BrokenRecordRequest> queueSystem)
        {
            _queueSystem = queueSystem;
        }
        //public BrokenRecordRequester(IPublishMessage messageSystem)
        //{
        //    _messageSystem = messageSystem;
        //}
        public override void Run()
        {
            while (!Stopped)
            {
                _queueSystem.MessageQueue.Enqueue(new BrokenRecordRequest { Message = "Marco?" });
                //_messageSystem.Publish("Marco", new BrokenRecordRequest {Message="Marco?"});
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
