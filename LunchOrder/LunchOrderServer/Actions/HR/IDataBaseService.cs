using LunchOrderServer.Models.Divisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderServer.Actions.HR
{
    interface IDataBaseService
    {
       // public Division FindDivisionByID();
        public string GetTemplate();
    }
}
