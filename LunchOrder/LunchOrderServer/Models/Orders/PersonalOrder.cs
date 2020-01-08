using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Models;
using LunchOrderServer.Models.Persons;

namespace LunchOrderServer.Models.Orders
{
    [CollectionName("PersonalOrders")]
    public class PersonalOrder : Entity
    {        
        public List<Food> FoodList { get; set; }
        public Employee Employee { get; set; }
        public PersonalOrder(Employee employee, List<Food> foodlist)
        {
            this.FoodList = foodlist;
            this.Employee = employee;
        }
    }
}
