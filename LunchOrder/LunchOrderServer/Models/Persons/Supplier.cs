using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer.Models.Orders;

namespace LunchOrderServer.Models.Persons
{
    public class Supplier
    {
        public string Name { get; set; }

        public List<Food> Foodlist {get; set;}

        public int Id_Supplier { get; set; }

        public Supplier(string name, List<Food> foodList)
        {
            this.Name = name;
            this.Foodlist = foodList;
        }

    }
}
