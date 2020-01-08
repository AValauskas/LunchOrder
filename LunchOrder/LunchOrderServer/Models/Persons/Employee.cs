using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Models;
using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Orders;

namespace LunchOrderServer.Models.Persons
{
    [CollectionName("Employees")]
    public class Employee : Entity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public string employee { get; set; }
        public string Division { get; set; }

        public Employee(string name, Division division)
        {
            this.Name = name;
         //   this.Username = username;
       //     this.Password = password;
         //   this.Division = division;
        }

        public Employee()
        {
            
        }



    }
}
