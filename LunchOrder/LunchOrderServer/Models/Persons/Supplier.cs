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

        public List<string> Foodlist { get; set; }

        public Supplier(string name, List<string> foodList)
        {
            this.Name = name;
            this.Foodlist = foodList;
        }
        public Supplier()
        {

        }

    }
}
