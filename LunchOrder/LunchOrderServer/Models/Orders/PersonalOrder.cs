using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer.Models.Persons;

namespace LunchOrderServer.Models.Orders
{
    public class PersonalOrder
    {        
        public List<Food> FoodList { get; set; }
        public Employee Employee { get; set; }
        public int Id_PersonalOrder { get; set; }
        public PersonalOrder(Employee employee, List<Food> foodlist)
        {
            this.FoodList = foodlist;
            this.Employee = employee;
        }
        //Pridėjimo metodas


        //Šalinimo metodas


        //Orderio pakeitimo metodas0
    }
}
