using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public interface IStartable
    {
        void Start();
    }

    public interface IStoppable
    {
        void Stop();
    }
}
