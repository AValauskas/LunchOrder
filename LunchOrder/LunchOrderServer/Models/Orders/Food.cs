using LunchOrderServer.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LunchOrderServer.Models.Orders.FoodEnum;

namespace LunchOrderServer.Models.Orders
{
    public class Food
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public foodTypes Foodtype;
        public int Count { get; set; }
        public int Id_Food { get; set; }

        public Food(string name, double cost, foodTypes foodtype)
        {
            this.Name = name;
            this.Cost = cost;
            this.Foodtype = foodtype;
        }
        public Food(string name, double cost, foodTypes foodtype, int count)
        {
            this.Name = name;
            this.Cost = cost;
            this.Foodtype = foodtype;
            this.Count = count;
        }
    }
}
