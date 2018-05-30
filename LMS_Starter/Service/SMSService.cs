using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Starter.Service
{
    public class SMSService : ISMSService
    {
        public SMSService()
        {

        }
        public async void SendMessage(string number, string message)
        {
            HttpClient getToken = new HttpClient();
            var tokenString = await getToken.GetStringAsync("https://sapi.telstra.com/v1/oauth/token?grant_type=client_credentials&client_id=qvizaP0VDd28GsXD6mPeI4GIJ1u34xlH&client_secret=LAQfIP1IUxvpo34s&scope=NSMS");
            var tokenObj = JObject.Parse(tokenString);
            var accessToken = tokenObj["access_token"].ToString();
            HttpClient getSendingNum = new HttpClient();
            getSendingNum.DefaultRequestHeaders.Add("authorization", $"Bearer {accessToken}");
            var getSendingNumBody = new JObject();
            var getSendingNumContent = new StringContent(getSendingNumBody.ToString(), Encoding.UTF8, "application/json");
            await getSendingNum.PostAsync("https://tapi.telstra.com/v2/messages/provisioning/subscriptions", getSendingNumContent);
            HttpClient sendMessage = new HttpClient();
            sendMessage.DefaultRequestHeaders.Add("authorization", $"Bearer {accessToken}");
            dynamic sendMessageBody = new JObject();
            sendMessageBody.to = number;
            sendMessageBody.body = message;
            var sendContent = new StringContent(sendMessageBody.ToString(), Encoding.UTF8, "application/json");
            await sendMessage.PostAsync("https://tapi.telstra.com/v2/messages/sms", sendContent);
        }
    }
}
