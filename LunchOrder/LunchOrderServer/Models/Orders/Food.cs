using CodeMash.Models;
using LunchOrderServer.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LunchOrderServer.Models.Orders.FoodEnum;

namespace LunchOrderServer.Models.Orders
{
    [CollectionName("Foods")]
    public class Food : Entity
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public foodTypes Foodtype;
        public int Count { get; set; }

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
        public Food()
        {
        }
    }
}
