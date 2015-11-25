using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chorea
{
    public abstract class ThreadedMicroserviceProcess : IStartable, IStoppable, IDisposable
    {
        public event EventHandler Starting;
        public event EventHandler Stopping;
        Thread _loopThread;
        protected bool Stopped { get; set; }
        public virtual void Start()
        {
            Stopped = false;
            Starting?.Invoke(this, new EventArgs());
            _loopThread = new Thread(Run);
            _loopThread.Start();
        }

        public virtual void Stop()
        {
            Stopped = true;
            Stopping?.Invoke(this, new EventArgs());
        }

        public virtual bool IsRunning => _loopThread != null && _loopThread.IsAlive;

        public abstract void Run();

        public void Dispose()
        {
            if (IsRunning) _loopThread?.Abort();
            _loopThread = null;
        }
    }
}
