using LunchOrderServer.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderServer.Actions.NotifyWorkers
{
   public interface INotificationSender
    {
        public void SendNotification(string templateId, List<string> receivers);
        public string GetTemplate(string templateId);
    }
}
