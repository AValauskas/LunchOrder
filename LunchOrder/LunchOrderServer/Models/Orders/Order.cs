using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Models;
using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Persons;

namespace LunchOrderServer.Models.Orders
{
    [CollectionName("Orders")]
    public class Order : Entity
    {
      //  public Supplier Supplier { get; set; }
        public List<Food> FoodList { get; set; }

        public DateTime LunchDay { get; set; }

        public List<Employee> Employees { get; set; }
        public List<Guest> Guests { get; set; }

        public List<PersonalOrder> EmployersOrders { get; set; }

        public bool IsOpen { get; set; }
      
        public double TotalPrice { get; set; }


        public Order(Menu menu)
        {
          //  this.FoodList = menu.foodlist;
            this.EmployersOrders = new List<PersonalOrder>();
           // this.Employees =menu.Employees;
          //  this.Guests = menu.Guests;
         //   this.LunchDay = menu.LunchTime;
            this.TotalPrice = 0;
            this.IsOpen = true;
        }

    }
}
