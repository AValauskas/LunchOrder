using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderServer.Actions.HR
{
   public interface IHrService
    {
        public LunchServiceSettings connections { get; set; }
        //   public void WritingToDatabase
        public List<Employee> IsWorkingAtLunchDay(string divisionId, DateTime lunchdate);
        //public List<Employee> GetAllEmployeesListWhoAreAnotherDivision();
    }
}
