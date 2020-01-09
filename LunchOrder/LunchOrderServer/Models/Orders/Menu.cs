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
        [JsonProperty(PropertyName = "Supplier")]
        public string ThismenuSupplier { get; set; }

        [JsonProperty(PropertyName = "Lunchtime2")]
        public float LunchTimeDate { get; set; }



        [JsonProperty(PropertyName = "Division")]
        public string DivisionThisMenuBelong { get; set; }


        [JsonProperty(PropertyName = "Foodlist")]
        public List<string> Menufoodlist { get; set; }


        [JsonProperty(PropertyName = "Employees")]
        public List<string> EmployeesInMenu { get; set; }

        [JsonProperty(PropertyName = "Guests")]
        public List<string> MenuGuests { get; set; }
        public Menu()
        {
            
        }
        public Menu(float lunchTime, List<string> employees, string division)
        {
            this.ThismenuSupplier = null;
            this.LunchTimeDate = lunchTime;
            this.DivisionThisMenuBelong = division;
            this.Menufoodlist = new List<string>();
            this.EmployeesInMenu = employees;
            this.MenuGuests = new List<string>();

        }
        public Menu(float lunchTime,  string division)
        {
            this.ThismenuSupplier = null;
            this.LunchTimeDate = lunchTime;
            this.DivisionThisMenuBelong = division;
            this.Menufoodlist = new List<string>();
            this.EmployeesInMenu = new List<string>();
            this.MenuGuests = new List<string>();

        }
        public Menu(DateTime lunchTime, List<string> employees, string division)
        {
            this.ThismenuSupplier = null;
           // this.LunchTimeDate = lunchTime;
            this.DivisionThisMenuBelong = division;
            this.Menufoodlist = new List<string>();
            this.EmployeesInMenu = employees;
            this.MenuGuests = new List<string>();

        }


    }
}
