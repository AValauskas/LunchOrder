using LunchOrderServer.Actions.NotifyWorkers;
using LunchOrderServer.Models.Divisions;
using LunchOrderServer.Models.Orders;
using LunchOrderServer.Models.Persons;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer;
using LunchOrderServer.Actions.Service;
using LunchOrderServer.Actions.HR;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;
using CodeMash.Client;

namespace LunchOrderServerTesting
{
    
    


    public class TestsOfLunchOrderServers
    {
        private string _lunchServiceApiKey = null;
        private Guid _lunchServiceID;

        IConfiguration Configuration { get; set; }
        //  const DateTime SaturdayDate = DateTime.Now.StartOfWeek(DayOfWeek.Saturday);
        public TestsOfLunchOrderServers()
        {
            var builder = new ConfigurationBuilder()
            .AddUserSecrets<Startup>();

            Configuration = builder.Build();

            var projectConfig = Configuration.GetSection("CodeMash")
                                .Get<LunchServiceSettings>();
            _lunchServiceApiKey = projectConfig.ApiKey;
            _lunchServiceID = Guid.Parse(projectConfig.ProjectId);

           // var client = new CodeMashClient(_lunchServiceApiKey, _lunchServiceID);

        }


        public void CreateMenuData(string countryName, string countryCode, string employeeName, string day, out Menu menu, out DateTime lunchdate)
        {
            var division = new Division(new Country(countryName, countryCode));
            DateTime closestFriday = DateTime.Now.StartOfWeek(DayOfWeek.Friday);
            var employeeList = new List<Employee>();
            employeeList.Add(new Employee(employeeName, division));
            menu = new Menu();
            var lunchTime = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day);
            lunchdate = DateTime.Now.StartOfWeek(lunchTime);

        }


        [Theory]
        [InlineData("Lietuva", "LTU", "Aurimas")]
        [InlineData("Lietuva", "LTU", "Karolis")]
        [InlineData("Lietuva", "LTU", "Lukas")]

        public void Create_Empty_Lunch_Menu_Success(string countryName, string countryCode, string employeeName)
        {

            var username = _lunchServiceApiKey;
            var password = _lunchServiceID;


            var division = new Division(new Country(countryName, countryCode));
            DateTime closestFriday = DateTime.Now.StartOfWeek(DayOfWeek.Friday);
            var employeeList = new List<Employee>();
            employeeList.Add(new Employee(employeeName, division));
            var menu = new Menu();          
            
            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsTodayMonday().Returns(true);
            
            var mockHrService = Substitute.For<IHrService>();
            //mockHrService.IsWorkingAtLunchDay(division.id, closestFriday).Returns(employeeList);
            lunchService.HRService = mockHrService;


        //    lunchService.Database = databaseService;

          //  var GainMenu = lunchService.CreateNewMenu(division);

         //   Assert.Equal(menu.LunchTime, GainMenu.LunchTime);
          //  Assert.Equal(menu.Employees, GainMenu.Employees);
        }


        [Theory]
        [InlineData("Lietuva", "LTU", "Aurimas")]
        [InlineData("Lietuva", "LTU", "Karolis")]
        [InlineData("Lietuva", "LTU", "Lukas")]

        public void Create_Empty_Lunch_Menu_Failed_Today_Not_Monday(string countryName, string countryCode, string employeeName)
        {
            var division = new Division(new Country(countryName, countryCode));
            DateTime closestFriday = DateTime.Now.StartOfWeek(DayOfWeek.Friday);
            var employeeList = new List<Employee>();
            employeeList.Add(new Employee(employeeName, division));
            var menu = new Menu();

            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsTodayMonday().Returns(false);

           // var exception = Assert.Throws<BussinessException>(() => lunchService.CreateNewMenu(division.id));

            var message = "Today is not Monday";
           // Assert.Equal(message, exception.Message);
        }


        [Theory]
        [InlineData("Lietuva", "LTU", "Aurimas","Saturday")]
        [InlineData("Lietuva", "LTU", "Karolis", "Sunday")]
        [InlineData("Lietuva", "LTU", "Lukas", "Monday")]        

        public void Adjust_Menu_LunchTime_Test_Wrong_Weekend_Selected_Test(string countryName, string countryCode, string employeeName, string day)
        {
            Menu menu;
            DateTime lunchDate;
            CreateMenuData(countryName, countryCode, employeeName, day, out menu, out lunchDate);

            ILunchService lunchService = new LunchService();

            var exception = Assert.Throws<BussinessException>(() => lunchService.AdjustMenuLunchTime(lunchDate, menu));

            var message = "Wrong date has been apllied becouse it is weekend";
            Assert.Equal(message, exception.Message);
        }


        [Theory]
        [InlineData("Lietuva", "LTU", "Aurimas", "Tuesday")]
        [InlineData("Lietuva", "LTU", "Karolis", "Wednesday")]
        [InlineData("Lietuva", "LTU", "Lukas", "Thursday")]

        public void Adjust_Menu_LunchTime_Test_Today_Is_Weekend(string countryName, string countryCode, string employeeName, string day)
        {
            Menu menu;
            DateTime lunchDate;
            CreateMenuData(countryName, countryCode, employeeName, day, out menu, out lunchDate);

            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsTodayWeekend().Returns(true);      
            
            var exception = Assert.Throws<BussinessException>(() => lunchService.AdjustMenuLunchTime(lunchDate, menu));

            var message = "Today you are not able to set a lunch day";
            Assert.Equal(message, exception.Message);
        }    


        [Theory]
        [InlineData("Lietuva", "LTU", "Aurimas", "Tuesday")]
        [InlineData("Lietuva", "LTU", "Karolis", "Wednesday")]
        [InlineData("Lietuva", "LTU", "Lukas", "Thursday")]
        public void Adjust_Menu_LunchTime_Test_Time_Has_Passed(string countryName, string countryCode, string employeeName, string day)
        {
            Menu menu;
            DateTime lunchDate;
            CreateMenuData(countryName, countryCode, employeeName, day, out menu, out lunchDate);

            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.TimeHasPassed(lunchDate).Returns(true);

            var exception = Assert.Throws<BussinessException>(() => lunchService.AdjustMenuLunchTime(lunchDate, menu));

            var SetDate = "Menu not created yet";
            Assert.Equal(SetDate, exception.Message);
        }


        [Fact]
        public void Supplier_Addition_To_Menu_Test_Failled_Date_Has_Passed()
        {
            var menu = TestCases.CreateEmptyMenu();

            ILunchService lunchService = Substitute.For<LunchService>();
           // lunchService.TimeHasPassed(menu.LunchTime).Returns(true);
            var exception = Assert.Throws<BussinessException>(() => lunchService.AddSupplierToMenu( menu, TestCases.GetExpressSupplier()));

            var message = "This is previous week menu";
            Assert.Equal(message, exception.Message);
        }


        [Fact]
        public void Supplier_Addition_To_Menu_Test_Failled_Supplier_Is_Null ()
        {
            var menu = TestCases.CreateEmptyMenu();
            //var arenalist = TestCases.ArenaList;
            ILunchService lunchService = Substitute.For<LunchService>();
          //  lunchService.TimeHasPassed(menu.LunchTime).Returns(false);
            var exception = Assert.Throws<BussinessException>(() => lunchService.AddSupplierToMenu(menu, null));

            var message = "Supplier is null";
            Assert.Equal(message, exception.Message);
        }
            
        
        [Fact]
        public void Supplier_Addition_To_Menu_Test_success()
        {            
            var menu = TestCases.CreateEmptyMenu();

            ILunchService lunchService = Substitute.For<LunchService>();
           // lunchService.TimeHasPassed(menu.LunchTime).Returns(false);

            var exception = Record.Exception(() => lunchService.AddSupplierToMenu(menu, TestCases.GetExpressSupplier()));
            Assert.Null(exception);
        }


        [Fact]
        public void Food_Addition_To_Menu_Test_Failed_Supplier_Not_Identified()
        {
            var menu = TestCases.CreateEmptyMenu();

            ILunchService lunchService = Substitute.For<LunchService>();
         //   lunchService.TimeHasPassed(menu.LunchTime).Returns(false);
          //  var exception = Assert.Throws<BussinessException>(() => lunchService.AddFoodToMenu(menu, TestCases.expressList));

            var message = "supplier not yet identified";
           // Assert.Equal(message, exception.Message);
        }


        [Fact]
        public void Food_Addition_To_Menu_Test_Failed_Not_All_Food_Countains()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
          //  lunchService.TimeHasPassed(menu.LunchTime).Returns(false);
           // var exception = Assert.Throws<BussinessException>(() => lunchService.AddFoodToMenu(menu, TestCases.ArenaList));

            var message = "the food which does not belong to chosen supplier was added";
           // Assert.Equal(message, exception.Message);
        }
               
        [Fact]
        public void Food_Addition_To_Menu_Test_success()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
          //  lunchService.TimeHasPassed(menu.LunchTime).Returns(false);

           // var exception = Record.Exception(() => lunchService.AddFoodToMenu(menu, TestCases.expressList));
         //   Assert.Null(exception);
        }


        [Fact]
        public void Guests_Addition_To_Menu_Test_Failed_No_Guests()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
           // lunchService.TimeHasPassed(menu.LunchTime).Returns(false);

            var exception = Assert.Throws<BussinessException>(() => lunchService.AddGuestToMenu(menu, new List<Guest>()));

            var message = "no guests were chosen";
            Assert.Equal(message, exception.Message);
        }


        [Fact]
        public void Guests_Addition_To_Menu_Test_Success()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
          //  lunchService.TimeHasPassed(menu.LunchTime).Returns(false);

            var exception = Record.Exception(() => lunchService.AddGuestToMenu(menu, TestCases.guestList));
            Assert.Null(exception);
        }


        [Fact]
        public void Guests_Remove_From_Menu_Test_Success()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
           // lunchService.TimeHasPassed(menu.LunchTime).Returns(false);

            var exception = Record.Exception(() => lunchService.RemoveGuest(menu, TestCases.guestList3));
            Assert.Null(exception);
        }
                                    

        [Fact]
        public void Create_Order_Time_Has_Passed()
        {
            var menu = TestCases.CreateEmptyMenu();

            ILunchService lunchService = Substitute.For<LunchService>();
          //  lunchService.TimeHasPassed(menu.LunchTime).Returns(true);

            var exception = Assert.Throws<BussinessException>(() => lunchService.CreateOrder(menu));

            var message = "This is previous week menu, set new menu";
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void Create_Order_Supplier_Not_Identified()
        {
            var menu = TestCases.CreateEmptyMenu();

            ILunchService lunchService = Substitute.For<LunchService>();
           // lunchService.TimeHasPassed(menu.LunchTime).Returns(false);

            var exception = Assert.Throws<BussinessException>(() => lunchService.CreateOrder(menu));

            var message = "supplier not yet identified";
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void Create_Order_Supplier_Failed_Food_List_Is_Empty()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();
       //     menu.FoodList = new List<Food>();

            ILunchService lunchService = Substitute.For<LunchService>();
          //  lunchService.TimeHasPassed(menu.LunchTime).Returns(false);

            var exception = Assert.Throws<BussinessException>(() => lunchService.CreateOrder(menu));

            var message = "Food List is empty, please set food list";
            Assert.Equal(message, exception.Message);
        }


        [Fact]
        public void Create_Order_Supplier_Failed_Employee_List_Is_empty()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();
            //menu.Employees = new List<Employee>();

            ILunchService lunchService = Substitute.For<LunchService>();
           // lunchService.TimeHasPassed(menu.LunchTime).Returns(false);

            var exception = Assert.Throws<BussinessException>(() => lunchService.CreateOrder(menu));

            var message = "Employees list is empty";
            Assert.Equal(message, exception.Message);
        }


        [Fact]
        public void Create_Order_Was_Succesfully()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
           // lunchService.TimeHasPassed(menu.LunchTime).Returns(false);

            var exception = Record.Exception(() => lunchService.CreateOrder(menu));
            Assert.Null(exception);
        }


        [Fact]
        public void PersonalOrder_Addition_Failed_Order_Is_Closed()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
            var exception = Assert.Throws<BussinessException>(() => lunchService.ComposePersonalOrder(TestCases.ClosedOrder(), TestCases.EmployeeFromLT1, TestCases.employee1List));

            var message = "Order is closed";
            Assert.Equal(message, exception.Message);
        }


        [Fact]
        public void PersonalOrder_addition_Failed_Choosen_food_list_is_empty()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
            var exception = Assert.Throws<BussinessException>(() => lunchService.ComposePersonalOrder(TestCases.OpenedOrder(), TestCases.EmployeeFromLT1, new List<Food>()));

            var message = "Your food list is empty";
            Assert.Equal(message, exception.Message);
        }


        [Fact]
        public void PersonalOrder_addition_success()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.ComposePersonalOrder(TestCases.OpenedOrder(), TestCases.EmployeeFromLT1, TestCases.employee1List);

            lunchService.Received().ComposePersonalOrder(TestCases.OpenedOrder(), TestCases.EmployeeFromLT1, TestCases.employee1List);
        }


        [Fact]
        public void PersonalOrder_Adjust_Success()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
            
            var exception = Record.Exception(() => lunchService.AdjustPersonalOrder(TestCases.OrderWithPersonalOrders(), TestCases.EmployeeFromLT1, TestCases.employee2List));
            Assert.Null(exception);
        }


        [Fact]
        public void Personal_Order_Delete_Succesfull()
        { 
            ILunchService lunchService = Substitute.For<LunchService>();
            var exception = Record.Exception(() => lunchService.CancelPersonalOrder(TestCases.OrderWithPersonalOrders(), TestCases.EmployeeFromLT1));
            Assert.Null(exception);
        }


        [Fact]
        public void Order_Closed_Failed_Empty_Personal_Order_List()
        {
            var order = TestCases.OrderWithPersonalOrders();
            order.EmployersOrders = new List<PersonalOrder>();

            ILunchService lunchService = Substitute.For<LunchService>();

            var exception = Record.Exception(() => lunchService.CloseOrder(order));

            var message = "Order is empty";
            Assert.Equal(message, exception.Message);
        }
                     

        [Fact]
        public void Order_Closed_Success_Test()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();

            var exception = Record.Exception(() => lunchService.CloseOrder(TestCases.OrderWithPersonalOrders()));
            Assert.Null(exception);         
        }


        [Fact]
        public void TTest_Check_If_Notification_Is_Needed_Failed_Lunch_Not_Tummorw()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsLunchTummorow(TestCases.OrderWithPersonalOrders().LunchDay).Returns(false);
           
            var exception = Assert.Throws<BussinessException>(() => lunchService.CheckIfNotificationIsNecesary(TestCases.OrderWithPersonalOrders()));

            var message = "It's too early to send message";
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void TTest_Check_If_Notification_Is_Needed_Failed_Order_Time_Passed()
        {
            var menu = TestCases.MenuwithFoodAndSupplier();

            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsLunchTummorow(TestCases.OrderWithPersonalOrders().LunchDay).Returns(true);
            lunchService.TimeHasPassed(TestCases.OrderWithPersonalOrders().LunchDay).Returns(true);

            var exception = Assert.Throws<BussinessException>(() => lunchService.CheckIfNotificationIsNecesary(TestCases.OrderWithPersonalOrders()));

            var message = "Order time has passed";
            Assert.Equal(message, exception.Message);
        }      
               
                
        [Fact]
        public void TTest_Check_If_Notification_Is_Needed_Success()
        {
            var order = TestCases.OrderWithPersonalOrders();

            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsLunchTummorow(order.LunchDay).Returns(true);
            lunchService.TimeHasPassed(order.LunchDay).Returns(false);
            lunchService.IsEmployeeOrderCompleted(order, TestCases.EmployeeFromLT1).Returns(true);
            lunchService.IsEmployeeOrderCompleted(order, TestCases.EmployeeFromLT2).Returns(false);        
            
            var notificationMkock = Substitute.For<INotificationSender>();

            var nCounter=0;
            notificationMkock.When(x => x.SendNotification("1", Arg.Any<List<string>>()))
         .Do(Xunit => nCounter++);
            lunchService.NotificationSender = notificationMkock;
            
            lunchService.CheckIfNotificationIsNecesary(TestCases.OrderWithPersonalOrders());
            Assert.Equal(1, nCounter);
        }


        [Fact]
        public void TTest_Check_If_Notification_That_Food_Arrived_Success()
        {
            var order = TestCases.OrderWithPersonalOrders();

            ILunchService lunchService = Substitute.For<LunchService>();

            var notificationMkock = Substitute.For<INotificationSender>();
            var nCounter = 0;
            notificationMkock.When(x => x.SendNotification("2", Arg.Any<List<string>>()))
         .Do(Xunit => nCounter++);
            lunchService.NotificationSender = notificationMkock;

            lunchService.SendNotificationThatFoodArrived(TestCases.OrderWithPersonalOrders());
            Assert.Equal(1, nCounter);
        }
        [Fact]
        public void Message_sending()
        {
            INotificationSender sendmes = new NotificationSender();

            sendmes.SendNotification("2", new List<string>());
        }

    }
}
