using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderServer.Actions.HR
{
   public interface IDataBaseService
    {
       // public Division FindDivisionByID();
        public string GetTemplate();

        public void WritingToDataBase(Object menu);

        public void UpdatingDataBase();
    }
}
