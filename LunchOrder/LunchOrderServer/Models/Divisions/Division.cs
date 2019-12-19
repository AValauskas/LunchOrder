using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer.Models.Persons;
using LunchOrderServer.Models.Orders;

namespace LunchOrderServer.Models.Divisions
{
    public class Division
    {
        public Country Country { get; set; }
        public List<Employee> Employees { get; set; }
        public string Adress { get; set; }
        public List<Order> Orders { get; set; }

        public Division(Country country)
        {
            this.Orders = new List<Order>();
            this.Employees = new List<Employee>();
            this.Country = country;
        }
    }
}
