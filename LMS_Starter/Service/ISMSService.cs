using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Starter.Service
{
    public interface ISMSService
    {
        void SendMessage(string number, string message);
    }
}
