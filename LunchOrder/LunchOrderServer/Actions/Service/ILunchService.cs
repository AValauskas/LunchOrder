using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer.Models.Orders;
using LunchOrderServer.Models.Persons;
using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Actions.HR;
using LunchOrderServer.Actions.Service;
using LunchOrderServer.Actions.NotifyWorkers;

namespace LunchOrderServer.Actions.Service
{
    public interface ILunchService
    {
        INotificationSender NotificationSender { get; set; }

        IHrService HRService { get; set; }

        public LunchServiceSettings connections { get; set; }

        //-------------------Menu--------------------
        //automatically each monday (add only theese employees who is not in holiday)---// use HrService
        public void CreateNewMenu(string divisionID); //

        //Change Menu time and renew employee list who are working in day which are set --/ 
        //If no exception were throwned, it means, that function passed succesfully, 
        public void AdjustMenuLunchTime(DateTime lunchtime, Menu menu);

        
        public void AddSupplierToMenu(Menu menu, Supplier supplier);


        public void AddFoodToMenu(Menu menu, List<string> food);

        public void AddEmployee(Menu menu, List<Employee> employees);
        public void RemoveEmployee(Menu menu, List<Employee> employees);

        //Will call HrService, to get list of employees who are working in another division
        public void AddGuestToMenu(Menu menu, List<Guest> guests);


        //arba public void RemoveGuest(Menu menu, List<string> employees);
        public void RemoveGuest(Menu menu, List<Guest> guests);


        //--------------------------------------------------------------
        //--------------Orders---------------------------------

        //then Menu is finished and it's a day before lunch day, this method convert menu to order
        public Order CreateOrder(Menu menu);


        //Person make his own chart of foood he want, and then add this to general Order

        public PersonalOrder ComposePersonalOrder(Order order, Employee employee, List<Food> foodlist);



        public void AdjustPersonalOrder(Order order, Employee employee, List<Food> foodlist);


        public void CancelPersonalOrder(Order order, Employee employee);

    
      
        public void CloseOrder(Order order);

        //Collapse all different personal orders food units, for example if we have one food in 10 different personal orders, it would be shown for admin only 1 food name and amaunt of them
       // public Order DisplayOrderToAdmin(Order order);

        //--------------------------------------------------------------
        //--------------Notification------------------------------------
        public void CheckIfNotificationIsNecesary(Order order);

        public void SendNotificationThatFoodArrived(Order order);




        //--------------------------------------------------------------
        //--------------Order Helpers---------------------------------

         public  bool IsLunchTummorow(DateTime lunchtime);

        public bool TimeHasPassed(DateTime lunchTime);

        public bool IsEmployeeOrderCompleted(Order order, Employee employee);

        public bool IsTodayMonday();

        public bool IsTodayWeekend();

        public bool DoMenuExist(string divisionId);
     
        public List<string> EmployeesWhoMadeAnOrder();

     //   public bool IsMenuCreated(DateTime lunchTime);
    }
}
