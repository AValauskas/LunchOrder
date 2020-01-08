using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Models;
using LunchOrderServer.Models.Orders;
using Newtonsoft.Json;

namespace LunchOrderServer.Models.Persons
{
    [CollectionName("Suppliers")]
    public class Supplier : Entity
    {
        public string Name { get; set; }
        [JsonProperty(PropertyName = "FoodList")]
        public List<Food> Foodlist {get; set;}

        public List<string> foodlist { get; set; }

        public Supplier(string name, List<Food> foodList)
        {
            this.Name = name;
            this.Foodlist = foodList;
        }
        public Supplier()
        {

        }

    }
}
