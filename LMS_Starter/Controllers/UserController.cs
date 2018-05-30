using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_Starter.Model;
using System.Net;
using LMS_Starter.Service;
using Newtonsoft.Json.Linq;

namespace LMS_Starter.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private ISMSService _sendSMS;
        private ILMSDataHandler _dbHandler;
        public UserController(ISMSService sendSMS,ILMSDataHandler dbHandler)
        {
            _sendSMS = sendSMS;
            _dbHandler = dbHandler;
        }
        // POST: api/user/register
        [HttpPost("register")]
        public void Register([FromBody]User user)
        {
            var newUser = LMS_Starter.Model.User.CreateUserFromBody(user);
            if (_dbHandler.Register(newUser))
            {
                string verificationCode = newUser.VerificationCode.ToString();
                _sendSMS.SendMessage(newUser.Phone, $"Your verification code is {verificationCode}.");
                Response.StatusCode = (int)HttpStatusCode.Created;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Conflict;
                Response.WriteAsync("Email address exist");
            }
        }

        // POST: api/user/login
        [HttpPost("login")]
        public IActionResult Login([FromBody]User user)
        {
            string token = _dbHandler.Login(user.Email,user.Password);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        // POST: api/user/sendLoginSMS
        [HttpPost("sendloginsms")]
        public IActionResult SendLoginSMS([FromBody]User user)
        {
            if (_dbHandler.IsUserVerified(user.Email))
            {
                int code = new Random().Next(100000, 999999);
                string phoneNo = _dbHandler.UpdateVerificationCodeAndReturnPhoneNo(user.Email, code);
                _sendSMS.SendMessage(phoneNo, $"Your login code is {code.ToString()}.");
                return Ok();
            }
            return Unauthorized();
        }

        // POST: api/user/loginWithSMS
        [HttpPost("loginwithsms")]
        public IActionResult SMSLogin([FromBody]LoginWithSMS user)
        {
            string token = _dbHandler.LoginWithSMS(user.Email, user.Code);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        // POST: api/user/verification
        [HttpPost("verification")]
        public IActionResult Verification([FromBody]User user)
        {
            string email = user.Email;
            string verificationCode = user.VerificationCode.ToString();
            if (email == null || verificationCode == "0")
            {
                return Unauthorized();
            }
            else if(_dbHandler.Verify(email,verificationCode))
            {
                return Ok("Successfully verified");
            }
            else
            {
                return Unauthorized();
            }
        }

    }
    public class LoginWithSMS
    {
        public string Email;
        public string Code;
    }
}
