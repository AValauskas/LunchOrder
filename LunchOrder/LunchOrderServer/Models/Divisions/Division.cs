using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer.Models.Persons;
using LunchOrderServer.Models.Orders;
using CodeMash.Models;
using Newtonsoft.Json;

namespace LunchOrderServer.Models.Divisions
{
    //[("Divisions")]
    public class Division : Entity
    {
         [JsonProperty(PropertyName = "temproraryCountry")]
        public Country Country { get; set; }
        public string name { get; set; }


       // [JsonProperty(PropertyName = "temproraryEmployee")]
        public List<Employee> Employees { get; set; }
        //public List<string> employees { get; set; }


        public string Adress { get; set; }
        public List<Order> Orders { get; set; }

        public Division(Country country)
        {
            this.Orders = new List<Order>();
            this.Employees = new List<Employee>();
            this.Country = country;
        }
        public Division()
        {
           
        }
    }
}
