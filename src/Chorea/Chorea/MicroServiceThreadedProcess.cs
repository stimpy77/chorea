using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chorea
{
    public abstract class MicroServiceThreadedProcess : IStartable, IStoppable, IDisposable
    {
        public event EventHandler Starting;
        public event EventHandler Stopping;
        Thread _loopThread;
        protected bool Stopped { get; set; }
        public virtual void Start()
        {
            this.Stopped = false;
            _loopThread = new Thread(Run);
            _loopThread.Start();
            this.Starting?.Invoke(this, new EventArgs());
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
