using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chorea;

namespace ConsoleInput
{
    public class UserCommandMessage : QueueMessage
    {
        public UserCommandMessage(string command)
        {
            MessageType = "UserCommand";
            Message = command;
        }
    }
}
