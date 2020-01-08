using CodeMash.Models;
using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Persons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderServer.Models.Orders
{
    [CollectionName("Menus")]
    public class Menu : Entity
    {
        [JsonProperty(PropertyName = "Supplier2")]
        public Supplier Suplier { get; set; }
        public string supplier { get; set; }


        [JsonProperty(PropertyName = "LunchTime3")]
        public DateTime LunchTime { get; set; }
        [JsonProperty(PropertyName = "lunchtime2")]
        public float LunchTimeDate { get; set; }
        public DateTime lunchtime { get; set; }


        [JsonProperty(PropertyName = "Division2")]
        public Division Division { get; set; }
        public string division { get; set; }


        [JsonProperty(PropertyName = "FoodList2")]
        public List<Food> FoodList { get; set; }
        public List<string> foodlist { get; set; }


        [JsonProperty(PropertyName = "Employees2")]
        public List<Employee> Employees { get; set; }

        public List<string> employees { get; set; }


        public List<Guest> Guests { get; set; }

        public Menu(DateTime lunchTime, List<Employee> employees, Division division)
        {
            this.Suplier = null;
            this.lunchtime = lunchTime;
            this.Division = division;
            this.FoodList = new List<Food>();
            this.Employees = employees;
            this.Guests = new List<Guest>();

        }

        public Menu()
        {
            
        }
        public Menu(DateTime lunchTime, List<string> employees, string division)
        {
            this.Suplier = null;
            this.lunchtime = lunchTime;
            this.division = division;
            this.FoodList = new List<Food>();
            this.employees = employees;
            this.Guests = new List<Guest>();

        }


    }
}
