using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer.Models.Orders;

namespace LunchOrderServer.Actions.NotifyWorkers
{
    public class NotificationSender: INotificationSender
    {
        public string GetTemplate(string templateId)
        {
            //
            //Message sql query     // if messages is in database.                  Maybe it's json file? 
            //
            throw new NotImplementedException();

        }
                                
        public void SendNotification(string templateId, List<string> receivers)
        {
            object message = GetTemplate(templateId);

            //TWILIO
          /*  string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("Twilio_AUTH_TOKEN");
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber("+37060984078");
            var from = new PhoneNumber("+12679302689");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "nice"
                );*/


            foreach (var receiver in receivers)
            {
                //
                //Send message to all receivers message with input receiver
                //
            }

        }
    }
}
