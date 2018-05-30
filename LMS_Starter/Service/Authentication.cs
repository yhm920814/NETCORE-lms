using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS_Starter.Model;

namespace LMS_Starter.Service
{
    public class Authentication:IAuthentication
    {
        private ILMSDataHandler _dbHandler;
        public Authentication(ILMSDataHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }
        public bool IsLogin(string token)
        {
            return _dbHandler.IsLogin(token);
        }
    }
}
