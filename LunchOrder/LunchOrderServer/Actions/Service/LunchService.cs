using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer.Actions.HR;
using LunchOrderServer.Actions.NotifyWorkers;
using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Orders;
using LunchOrderServer.Models.Persons;

namespace LunchOrderServer.Actions.Service
{
    public class LunchService : ILunchService
    {
        public IHrService HRService { get; set; }
        public INotificationSender NotificationSender { get; set; }



        //---------------Menu----------------

        public Menu CreateNewMenu(Division division)
        {
            
            if (IsTodayMonday())
            {
                DateTime closestFriday = DateTime.Now.StartOfWeek(DayOfWeek.Friday);
                List<Employee> employees = HRService.IsWorkingAtLunchDay(division, closestFriday);
                var menu = new Menu(closestFriday, employees, division);
                //
                //Writing to database
                //
                return menu;
            }
            else {
                throw new BussinessException("Today is not Monday");
            }             
        }      
        public void AdjustMenuLunchTime(DateTime lunchtime, Menu menu)
        {
            if (lunchtime.DayOfWeek == DayOfWeek.Saturday || lunchtime.DayOfWeek == DayOfWeek.Sunday || lunchtime.DayOfWeek == DayOfWeek.Monday)
            {
                throw new BussinessException("Wrong date has been apllied becouse it is weekend");
            }           
            //this check was made becouse menu automatically creates only in mondays, so if it's unneccesary, we need to make check in automatically create method,
            //if there is no menu already created
            if (IsTodayWeekend())
            {
                throw new BussinessException("Today you are not able to set a lunch day");
            }
            /*  if (IsMenuCreated(menu.LunchTime))
              {
                  throw new BussinessException("Menu not created yet");
              }*/
            if (TimeHasPassed(lunchtime))
            {
                throw new BussinessException("this lunch date has passed, chooose another day");
            }            
            else
            {
                menu.LunchTime = lunchtime;
                List<Employee> employees = HRService.IsWorkingAtLunchDay(menu.Division, lunchtime);
                menu.Employees = employees;
                //
                //Writing to database
                //
            }
        }      
        public void AddSupplierToMenu(Menu menu, Supplier supplier)
        {
            //Checking if menu is from this week, not previous
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            if (supplier == null)
            {
                throw new BussinessException("Supplier is null");
            }
            menu.Suplier = supplier;

            //
            //updating menu in database
            //
        }
        public void AddFoodToMenu(Menu menu, List<Food> food)
        {
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            if (menu.Suplier == null)
            {
                throw new BussinessException("supplier not yet identified");
            }
            bool hasAll = food.Intersect(menu.Suplier.Foodlist).Count() == food.Count();
            if (!hasAll)
            {
                throw new BussinessException("the food which does not belong to chosen supplier was added");
            }
            menu.FoodList = food;
            //
            //Update menu in database
            //              
        }
        public void AddGuestToMenu(Menu menu, List<Guest> guests)
        {
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            if (guests.Count== 0)
            {
                throw new BussinessException("no guests were chosen");
            }

            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            guests.RemoveAll(x => menu.Guests.Any(y => y.Id_Guest == x.Id_Guest));
            
            menu.Guests = menu.Guests
            .Concat(guests)
            .ToList();
            //
            //update database
            //

        }
        public void RemoveGuest(Menu menu, List<Guest> guests)
        {
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            if (guests.Count == 0)
            {
                throw new BussinessException("no guests were chosen");
            }
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            menu.Guests.RemoveAll(x => guests.Any(y => y.Id_Guest == x.Id_Guest));
            //
            //update database
            //
        }
        public void AddEmployee(Menu menu, List<Employee> employees)
        {
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            if (employees.Count == 0)
            {
                throw new BussinessException("no guests were chosen");
            }
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            employees.RemoveAll(x => menu.Employees.Any(y => y.Name == x.Name));

            menu.Employees = menu.Employees
           .Concat(employees)
           .ToList();
        }

        public void RemoveEmployee(Menu menu, List<Employee> employees)
        {
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            if (employees.Count == 0)
            {
                throw new BussinessException("no guests were chosen");
            }
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu");
            }
            menu.Employees.RemoveAll(x => employees.Any(y => y.Name == x.Name));
        }

        //------------------------------------------------------
        //-----------------------Order---------------------------

        public Order CreateOrder(Menu menu)//Maybe we can get division from menu, becouse menu has division attribute.
        {
            if (TimeHasPassed(menu.LunchTime))
            {
                throw new BussinessException("This is previous week menu, set new menu");
            }
            if (menu.Suplier==null)
            {
                throw new BussinessException("supplier not yet identified");
            }
            if (menu.FoodList.Count == 0)
            {
                throw new BussinessException("Food List is empty, please set food list");
            }
            if (menu.Employees.Count == 0)
            {
                throw new BussinessException("Employees list is empty");
            }

            Order order = new Order(menu);
           // menu.Division.Orders.Add()
            menu.Division.Orders.Add(order);
            //
            //Create order in database
            //
            return order;
        }


        public PersonalOrder ComposePersonalOrder(Order order, Employee employee, List<Food> foodlist)
        {
            if (order.IsOpen==false)
            {
                throw new BussinessException("Order is closed");
            }
            if (foodlist.Count == 0)
            {
                throw new BussinessException("Your food list is empty");
            }
            PersonalOrder personalOrder = new PersonalOrder(employee, foodlist);
            order.EmployersOrders.Add(personalOrder);


            //
            //update orders in database by adding personalorder
            //
                                 
            return personalOrder;
        }

        public void AdjustPersonalOrder(Order order, Employee employee, List<Food> foodlist)// instead of personalorder we can use here employee too
        {
            if (order.IsOpen == false)
            {
                throw new BussinessException("Order is closed");
            }
            if (foodlist.Count == 0)
            {
                throw new BussinessException("Your food list is empty");
            }


            var index = order.EmployersOrders.FindIndex(Eo => Eo.Employee.Name == employee.Name);//Change to id
               order.EmployersOrders[index].FoodList = foodlist;

            //
            //Update PersonalOrder in database
            //
        }
        //Create something to check if employee has an order
        public void CancelPersonalOrder(Order order, Employee employee)
        {
            if (order.IsOpen == false)
            {
                throw new BussinessException("Order is closed");
            }
            order.EmployersOrders.RemoveAt(order.EmployersOrders.FindIndex(Eo => Eo.Employee.Name == employee.Name));

            //
            //Delete PersonalOrder from database
            //
        }
        public void CloseOrder(Order order)
        {
            if (order.EmployersOrders.Count == 0)
            {
                throw new BussinessException("Order is empty");
            }
            order.IsOpen = false;
            //
            //update database
            //
        }     



        //------------------------------------------------------
        //-----------------------Notify---------------------------

        public void CheckIfNotificationIsNecesary(Order order)
        {
            var isLunchTummorochecker = IsLunchTummorow(order.LunchDay);
            if (!isLunchTummorochecker)
            {
                throw new BussinessException("It's too early to send message");
            }

            var isTimePassed = TimeHasPassed(order.LunchDay);
            if (isTimePassed)
            {
                throw new BussinessException("Order time has passed");
            }
            List<string> Receivers = new List<string>();
            foreach (var employee in order.Employees)
            {
                var isPersonalOrderCompleted = IsEmployeeOrderCompleted(order, employee);
                if ( !isPersonalOrderCompleted)
                {
                    Receivers.Add(employee.Name);//name change to id
                }
            }
            NotificationSender.SendNotification("1",Receivers);

            //1 is message which will be sent if the order is not yet completed
        }
        public void SendNotificationThatFoodArrived(Order order)
        {
            List<string> Receivers = new List<string>();
            
            foreach (var personalOrder in order.EmployersOrders)
            {
                Receivers.Add(personalOrder.Employee.Name);
            }
            //or
            // List<string> Receivers = GetReceiverList()<------//helper method 
                       
            NotificationSender.SendNotification("2", Receivers);

            //uses the same interface as a checker, to send messages. sends a message which are marked as 2 , and it means, that the lunch has arrived
        }
                          






        //------------------------------------------------------
        //-----------------------Helper---------------------------
        public virtual bool IsLunchTummorow(DateTime lunchtime)
        {
            var difference = (lunchtime - DateTime.Now).Days+1;
               

            if (difference == 1)
            {
                return true;
            }
            return false;
        }

        public virtual bool TimeHasPassed(DateTime lunchTime)
        {
            int constant = DateTime.Now.CompareTo(lunchTime);
            if (constant<0)
            {
                return true;
            }
            return false;
        }
               
        public virtual bool IsEmployeeOrderCompleted(Order order, Employee employee)
        {
            var personalorder = order.EmployersOrders.Select(personalOrder => personalOrder.Employee.Id_Employee == employee.Id_Employee).First();
            if (personalorder != null)
            {
                return true;
            }
            return false;
        }

        public virtual bool IsTodayMonday()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                return true;
            }
            return false;
        }

        public virtual bool IsTodayWeekend()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday|| DateTime.Now.DayOfWeek == DayOfWeek.Sunday|| DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                return true;
            }
            return false;
        }
    /*    public virtual bool IsMenuCreated(DateTime lunchTime)
        {
            throw new NotImplementedException();
        }
        */

        public virtual List<string> EmployeesWhoMadeAnOrder()
        {
            throw new NotImplementedException();
        }

      
    }
}
