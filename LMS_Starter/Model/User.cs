using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Starter.Model
{
    public class User
    {

        public string Password { get; set; }
        public string Token { get; set; }
        public int VerificationCode { get; set; }
        public bool IsVerified { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public static User CreateUserFromBody(User user)
        {
            User newUser = new User();
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            newUser.Phone = user.Phone;
            newUser.Token = "";
            newUser.IsVerified = false;
            newUser.VerificationCode = new Random().Next(100000, 999999);
            return newUser;
        }
        public bool IsPasswordMatch(string password)
        {
            if(password == Password)
            {
                return true;
            }
            return false;
        }
        public bool IsCodeMatch(string code)
        {
            if (code == VerificationCode.ToString())
            {
                return true;
            }
            return false;
        }
        public string GenerateToken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            Token = token;
            return token;
        }

        public void Verify()
        {
            IsVerified = true;
        }
    }
}
