using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Orders;

namespace LunchOrderServer.Models.Persons
{
    public class Employee
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Id_Employee { get; set; }
        public Division Division { get; set; }
      //  public int Role { get; set; }//  1 vadovas/ 2 administratorius/ 3 darbuotojas/ 4 praktikantas

        public Employee(string name, Division division)
        {
            this.Name = name;
         //   this.Username = username;
       //     this.Password = password;
            this.Division = division;
        }


    }
}
