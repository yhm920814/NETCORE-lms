using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Starter.Service
{
    public interface IAuthentication
    {
        bool IsLogin(string token);
    }
}
