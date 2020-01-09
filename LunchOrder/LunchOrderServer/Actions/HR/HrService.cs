using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Repository;
using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Persons;
using MongoDB.Driver;

namespace LunchOrderServer.Actions.HR
{
    public class HrService : IHrService
    {
        public LunchServiceSettings connections { get; set; }
        public List<Employee> IsWorkingAtLunchDay(string divisionId, DateTime lunchdate)
        {


            var projectId = Guid.Parse(connections.ProjectId);
            var apiKey = connections.ApiKey;
            var client = new CodeMashClient(apiKey, projectId);
            var service = new CodeMashRepository<Employee>(client);

         //   var filter = Builders<Employee>.Filter.Eq(x => x.Division, divisionId);
         //   var result = service.Find(filter).Result.ToList();

        //   var persons = service.Find(x => x.Division == divisionId,
             //    new DatabaseFindOptions());

            var persons = service.Find().Result.ToList();
            var workingEmployeeList = persons.FindAll(x => x.Division == divisionId).ToList();

            return workingEmployeeList;
        }
    }
}
