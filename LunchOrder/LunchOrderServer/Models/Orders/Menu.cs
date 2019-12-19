using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderServer.Models.Orders
{
    public class Menu
    {
        public Supplier Suplier { get; set; }

        public DateTime LunchTime { get; set; }

        public Division Division { get; set; }

        public List<Food> FoodList { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Guest> Guests { get; set; }
        public int Id_Menu { get; set; }

        public Menu(DateTime lunchTime, List<Employee> employees, Division division)
        {
            this.Suplier = null;
            this.LunchTime = lunchTime;
            this.Division = division;
            this.FoodList = new List<Food>();
            this.Employees = employees;
            this.Guests = new List<Guest>();

        }

        

    }
}
