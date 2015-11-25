using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Chorea.Windows
{
    public class EventPumpMessageEventDispatcher<TMessage> : EventPumpMicroserviceProcess, IMessageEventDispatcher<TMessage>, IDisposable
    {

        public EventPumpMessageEventDispatcher(string route = null)
        {
            _route = route;
            Starting += OnStarting;
            Stopping += OnStopping;
        }

        void OnStarting(object sender, EventArgs eventArgs)
        {
            foreach (IStartable startable in _microServices.Where(service => service is IStartable))
                startable.Start();
        }

        void OnStopping(object sender, EventArgs eventArgs)
        {
            foreach (IStoppable stoppable in _microServices.Where(service => service is IStoppable))
            {
                stoppable.Stop();
                Thread.Yield();
            }
        }

        public event EventHandler<MessageEventArgs<TMessage>> MessageReceived;
        readonly List<object> _microServices = new List<object>();
        private readonly string _route;

        public virtual void RegisterMessageSource(object service)
        {
            _microServices.Add(service);
        }

        public IList<object> MessageSources => _microServices;

        public override void Tick()
        {
            // iterate over known IHasMessageQueue services
            foreach (IHasBclMessageQueue<TMessage> service in _microServices.Where(s => s is IHasBclMessageQueue<TMessage>))
            {
                // flush this service's queue
                while (!service.MessageQueue.IsEmpty)
                {
                    TMessage message;
                    if (service.MessageQueue.TryDequeue(out message))
                        MessageReceived?.Invoke(this, new MessageEventArgs<TMessage>(service.QueueName, message));
                }
            }
            // iterate over known IHasPublishedMessages services
            foreach (IHasPublishedMessages<TMessage> service in _microServices.Where(s => s is IHasPublishedMessages<TMessage>))
            {
                // flush this service's queue
                //var message = service.MessageQueue.Dequeue();
                //MessageReceived?.Invoke(this, new MessageEventArgs(message));
                foreach (var message in service.PublishedMessages.GetAllPublishedMessagesSinceLast(_route))
                {
                    MessageReceived?.Invoke(this, new MessageEventArgs<TMessage>(message));
                }
            }
        }

        void IDisposable.Dispose()
        {
            if (!Stopped) Stop();
            foreach (IStoppable stoppable in _microServices.Where(service => service is IStoppable))
                stoppable.Stop();
            foreach (IDisposable disposable in _microServices.Where(service => service is IDisposable))
                disposable.Dispose();
        }

        public override void Pause()
        {
            base.Pause();
            foreach (IPausable process in _microServices.Where(service => service is IPausable))
            {
                process.Pause();
            }
        }

        public override void Continue()
        {
            base.Continue();
            foreach (IPausable process in _microServices.Where(service => service is IPausable))
            {
                process.Continue();
            }
        }
    }
}
