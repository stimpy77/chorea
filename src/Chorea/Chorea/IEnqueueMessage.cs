﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public interface IEnqueueMessage
    {
        void EnqueueMessage(object message);
    }
}
