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
using CodeMash.Repository;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using MongoDB.Driver.Builders;

namespace LunchOrderServerTesting
{
         
    public class IntegrationalTests
    {
        private LunchServiceSettings settings = new LunchServiceSettings();

        IConfiguration Configuration { get; set; }
        public IntegrationalTests()
        {
            var builder = new ConfigurationBuilder()
            .AddUserSecrets<Startup>();

            Configuration = builder.Build();

            var projectConfig = Configuration.GetSection("CodeMash")
                                .Get<LunchServiceSettings>();
            settings.ApiKey = projectConfig.ApiKey;
            settings.ProjectId = projectConfig.ProjectId;
        }

       [Fact]
        public void Create_Empty_Lunch_Menu_Success()
        {
            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsTodayMonday().Returns(true);

            IHrService hrService = new HrService();
            hrService.connections = settings;

            lunchService.HRService = hrService;
            lunchService.connections = settings;

            lunchService.CreateNewMenu(TestCases.MyDivisionID);

        }

        [Fact]
        public void Adjust_Lunch_Time_Menu_Success()
        {

            var projectId = Guid.Parse(settings.ProjectId);
            var apiKey = settings.ApiKey;
            var client = new CodeMashClient(apiKey, projectId);
            var service = new CodeMashRepository<Menu>(client);

            var menuList = service.Find().Result.ToList();
            var menuByDivision = menuList.FindAll(x => x.division == TestCases.MyDivisionID).ToList();
            var lastMenu = menuByDivision[menuList.Count - 1];

            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsTodayMonday().Returns(true);

            IHrService hrService = new HrService();
            hrService.connections = settings;

            lunchService.HRService = hrService;
            lunchService.connections = settings;

            lunchService.AdjustMenuLunchTime(TestCases.Thursday, lastMenu);

        }

        [Fact]
        public void Add_Supplier_To_Menu_Success()
        {
            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsTodayMonday().Returns(true);

            IHrService hrService = new HrService();
            hrService.connections = settings;

            lunchService.HRService = hrService;
            lunchService.connections = settings;


            var projectId = Guid.Parse(settings.ProjectId);
            var apiKey = settings.ApiKey;
            var client = new CodeMashClient(apiKey, projectId);
            var serviceMenu = new CodeMashRepository<Menu>(client);
            var serviceSupplier = new CodeMashRepository<Supplier>(client);


            var menuList = serviceMenu.Find().Result.ToList();
            var menuByDivision = menuList.FindAll(x => x.division == TestCases.MyDivisionID).ToList();
            var lastMenu = menuByDivision[menuList.Count - 1];

            var supplierList = serviceSupplier.Find().Result.ToList();
            Random rnd = new Random();
            var randomSupplier = supplierList[rnd.Next(0, supplierList.Count-1)];                                            

            lunchService.AddSupplierToMenu(lastMenu, randomSupplier);
        }



        [Fact]
        public void Add_Food_To_Menu_Success()
        {
            ILunchService lunchService = Substitute.For<LunchService>();
            lunchService.IsTodayMonday().Returns(true);

            IHrService hrService = new HrService();
            hrService.connections = settings;

            lunchService.HRService = hrService;
            lunchService.connections = settings;


            var projectId = Guid.Parse(settings.ProjectId);
            var apiKey = settings.ApiKey;
            var client = new CodeMashClient(apiKey, projectId);
            var serviceMenu = new CodeMashRepository<Menu>(client);
            var serviceSupplier = new CodeMashRepository<Supplier>(client);
            var serviceFood = new CodeMashRepository<Food>(client);

            var menuList = serviceMenu.Find().Result.ToList();
            var menuByDivision = menuList.FindAll(x => x.division == TestCases.MyDivisionID).ToList();
            var lastMenu = menuByDivision[menuList.Count - 1];

            var supplier = serviceSupplier.FindOneById(lastMenu.supplier).Result;

            var foodList = serviceFood.Find().Result.ToList();
            var foodToAddList = foodList.FindAll(x => supplier.foodlist.Contains(x.Id)).ToList();

            lunchService.AddFoodToMenu(lastMenu, supplier.foodlist);
        }



        [Fact]
        public void Test_Menu_Find()
        {        
            var projectId = Guid.Parse(settings.ProjectId);
            var apiKey = settings.ApiKey;
            var client = new CodeMashClient(apiKey, projectId);
            var serviceMenu = new CodeMashRepository<Menu>(client);

            var meniufind = serviceMenu.Find().Result.ToList();
            double timespan = meniufind[1].LunchTimeDate;

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0);
            DateTime date = epoch.AddMilliseconds(timespan);


            
        }














    }
}
