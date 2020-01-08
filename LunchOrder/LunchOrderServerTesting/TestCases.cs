using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Orders;
using LunchOrderServer.Models.Persons;
using System;
using System.Collections.Generic;
using System.Text;

namespace LunchOrderServerTesting
{
    public class TestCases : IDisposable
    {

        public static string MyDivisionID = "5e144e04e39c590001d31fcd";
        //DivisionsData
        public static readonly Division DivisionLTU = new Division(new Country("Lithuania", "LTU"));
        public static readonly Division DivisionNL = new Division(new Country("Lithuania", "NL"));
        //Emplyees data
        public  static readonly Employee EmployeeFromLT1 = new Employee("employee1", DivisionLTU);
        public static readonly Employee EmployeeFromLT2 = new Employee("employee2", DivisionLTU);
        public static readonly Employee EmployeeFromLT3Guest = new Employee("employee3", DivisionLTU);
        public static readonly Employee EmployeeFromNLGuest = new Employee("employee34",  DivisionNL);

        public static readonly PersonalOrder employee1Order = new PersonalOrder(EmployeeFromLT1, employee1List);
        public static readonly PersonalOrder employee2Order = new PersonalOrder(EmployeeFromLT2, employee2List);

        public static Menu CreateEmptyMenu()
        {
          
    var employeeList = new List<Employee> { EmployeeFromLT1, EmployeeFromLT2, EmployeeFromLT3Guest };
            //mployeeList.Add(EmployeeFromLT1);
            Menu menu = new Menu(Friday, employeeList, DivisionLTU);
            menu.LunchTime = Friday;
          //  menu.division = "5e0db73559c49d0001b6160f";
            menu.Id = "5e0df4d259c49d0001c76255";
            return menu;
        }

        public static Menu MenuwithFoodAndSupplier()
        {

            var employeeList = new List<Employee> { EmployeeFromLT1, EmployeeFromLT2, EmployeeFromLT3Guest };
            //mployeeList.Add(EmployeeFromLT1);
            Menu menu = new Menu(Friday, employeeList, DivisionLTU);
         //   menu.division = "5e0db73559c49d0001b6160f";
            menu.Id = "5e0df4d259c49d0001c76255";
        //    menu.supplier = "5e0db7ee59c49d0001b61652";
            menu.LunchTime = Friday;
            menu.Suplier = GetExpressSupplier();
            menu.FoodList = expressList;
            menu.Guests = guestList2;
            return menu;
        }

        public static Order ClosedOrder()
        {
            Order order = new Order(MenuwithFoodAndSupplier());
            order.IsOpen = false;
            return order;        
        }
        public static Order OpenedOrder()
        {
            Order order = new Order(MenuwithFoodAndSupplier());
            return order;
        }
        public static PersonalOrder PersonalOrderToChange()
        {
            PersonalOrder personal = new PersonalOrder(EmployeeFromLT1, employee1List);
            return personal;
        }
        public static Order OrderWithPersonalOrders()
        {
            Order order = new Order(MenuwithFoodAndSupplier());
           
            PersonalOrder personal = new PersonalOrder(EmployeeFromLT1, employee1List);
            PersonalOrder personal2 = new PersonalOrder(EmployeeFromLT2, employee2List);
            order.EmployersOrders.Add(personal);
            order.EmployersOrders.Add(personal2);
            return order;
        }
        public static List<string> Receivers()
        {
            List<string> Receivers = new List<string>();

            foreach (var personalOrder in OrderWithPersonalOrders().EmployersOrders)
            {
                Receivers.Add(personalOrder.Employee.Name);
            }
            return Receivers;
        }
     


        //Wrong date
        public static readonly DateTime SaturdayDate = DateTime.Now.StartOfWeek(DayOfWeek.Saturday);
        public static readonly DateTime SundayDate = DateTime.Now.StartOfWeek(DayOfWeek.Saturday);
        public static readonly DateTime MondayDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        //good dates
        public static readonly DateTime Wednesday = DateTime.Now.StartOfWeek(DayOfWeek.Wednesday);
        public static readonly DateTime Thursday = DateTime.Now.StartOfWeek(DayOfWeek.Thursday);
        public static readonly DateTime Friday = DateTime.Now.StartOfWeek(DayOfWeek.Friday);
        public static readonly DateTime Tuesday = DateTime.Now.StartOfWeek(DayOfWeek.Tuesday);

        //Suppliers 
    

        //Food List
        public static readonly List<Food> expressList = new List<Food>() {
            new Food("Peperoni", 10.5,FoodEnum.foodTypes.MainDish),
            new Food("Cola", 0.7, FoodEnum.foodTypes.Drink),
            new Food("Soup", 1.5,FoodEnum.foodTypes.Soup),
            new Food("soft", 8.5,FoodEnum.foodTypes.Souce) };

        public static readonly List<Food> employee1List = new List<Food>() {
            new Food("Peperoni", 10.5,FoodEnum.foodTypes.MainDish),
            new Food("Soup", 1.5,FoodEnum.foodTypes.Soup) };

        public static readonly List<Food> employee2List = new List<Food>() {
            new Food("Peperoni", 10.5,FoodEnum.foodTypes.MainDish),
              new Food("Cola", 0.7, FoodEnum.foodTypes.Drink),
            new Food("Soup", 1.5,FoodEnum.foodTypes.Soup) };

        public static readonly List<Food> ArenaList = new List<Food>() { 
            new Food("Margarita", 10.5, FoodEnum.foodTypes.MainDish), 
            new Food("Pepsi", 0.7, FoodEnum.foodTypes.Drink),
            new Food("Salty", 1.5,FoodEnum.foodTypes.Soup), 
            new Food("garlic", 9.5,FoodEnum.foodTypes.Souce) };

        public static readonly List<Guest> guestList = new List<Guest>() {
            new Guest("Antanas"),
            new Guest("Jonas"),
            new Guest("Jurgis"),
            new Guest("Petras"), };
        public static readonly List<Guest> guestList2 = new List<Guest>() {
            new Guest("Marius"),
            new Guest("Darius"),
            new Guest("Petras"), };
        public static readonly List<Guest> guestList3 = new List<Guest>() {
            new Guest("Marius"),
            new Guest("Jonas"),
            new Guest("Jurgis"),};

        public static Supplier GetArrenaSupplier() { Supplier ArennaSupplier = new Supplier("Arenna", ArenaList);return ArennaSupplier;
    }
        public static Supplier GetExpressSupplier()
        {
            Supplier ExpressSupplier = new Supplier("Express", expressList);
            ExpressSupplier.Id = "5e0db7ee59c49d0001b61652";
            return ExpressSupplier;
        }




        public void Dispose()
        {
            // clean up test data after each tests
        }
    }
}
