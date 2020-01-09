using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Models;
using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Persons;
using Newtonsoft.Json;

namespace LunchOrderServer.Models.Orders
{
    [CollectionName("Orders")]
    public class Order : Entity
    {
        public string Supplier { get; set; }
        [JsonProperty(PropertyName = "foodlist")]
        public List<string> Orderfoodlist { get; set; }

        //[JsonProperty(PropertyName = "lunchday")]
        public DateTime LunchDay { get; set; }
        public List<string> Employees { get; set; }
        public List<string> Guests { get; set; }

        public List<string> EmployeersOrders { get; set; }

        public bool IsOpen { get; set; }
      
        //public double TotalPrice { get; set; }


        public Order(Menu menu)
        {
            this.Orderfoodlist = menu.Menufoodlist;
            this.EmployeersOrders = new List<string>();
            this.Employees =menu.EmployeesInMenu;
            this.Guests = menu.MenuGuests;
            this.LunchDay = DateTime.Now;
            //this.TotalPrice = 0;
            this.IsOpen = true;
            this.Supplier = menu.ThismenuSupplier;
        }
        public Order() { }
    }
}
