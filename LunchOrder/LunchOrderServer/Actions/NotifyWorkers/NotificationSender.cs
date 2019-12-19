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
            string message = GetTemplate(templateId);

            //
            //Send message to all receivers
            //

            throw new NotImplementedException();
        }
    }
}
