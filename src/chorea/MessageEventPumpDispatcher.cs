using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chorea
{
    public class MessageEventPumpDispatcher : MicroServiceEventPumpTimerProcess, IDisposable
    {

        public MessageEventPumpDispatcher()
        {
            Starting += OnStarting;
            Stopping += OnStopping;
        }

        void OnStarting(object sender, EventArgs eventArgs)
        {
            foreach (var startable in _microServices.Select(service => service as IStartable))
                startable?.Start();
        }

        void OnStopping(object sender, EventArgs eventArgs)
        {
            foreach (var stoppable in _microServices.Select(service => service as IStoppable))
            {
                stoppable?.Stop();
                Thread.Yield();
            }
        }

        public event EventHandler<MessageEventArgs> MessageReceived;
        readonly List<object> _microServices = new List<object>();

        public virtual void RegisterMessageSource(object service)
        {
            _microServices.Add(service);
        }

        public IList<object> MessageSources => _microServices;

        public override void Tick()
        {
            // iterate over known IHasMessageQueue services
            foreach (IHasMessageQueue service in _microServices.Where(s => s is IHasMessageQueue))
            {
                // flush this service's queue
                while (!service.MessageQueue.IsEmpty)
                {
                    object message;
                    if (service.MessageQueue.TryDequeue(out message))
                        MessageReceived?.Invoke(this, new MessageEventArgs(message));
                }
            }
            // iterate over known IHasPublishedMessages services
            foreach (IHasPublishedMessages service in _microServices.Where(s => s is IHasPublishedMessages))
            {
                // flush this service's queue
                //var message = service.MessageQueue.Dequeue();
                //MessageReceived?.Invoke(this, new MessageEventArgs(message));
                foreach (var message in service.PublishedMessages.GetPublishedMessagesSinceLast())
                {
                    MessageReceived?.Invoke(this, new MessageEventArgs(message));
                }
            }
        }

        void IDisposable.Dispose()
        {
            if (!Stopped) Stop();
            foreach (var stoppable in _microServices.Select(service => service as IStoppable))
                stoppable?.Stop();
            foreach (var disposable in _microServices.Select(service => service as IDisposable))
                disposable?.Dispose();
        }
    }
}
