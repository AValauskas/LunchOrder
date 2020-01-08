using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchOrderServer.Actions.HR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CodeMash.Client;
using CodeMash.Repository;
using MongoDB.Driver;
using LunchOrderServer.Models.Persons;
using CodeMash.Project.Services;
using Isidos.CodeMash.ServiceContracts;

namespace LunchOrderServer
{
    public class Startup
    {
        private string _lunchServiceApiKey = null;
        private Guid _lunchServiceID;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var moviesConfig = Configuration.GetSection("CodeMash")
                                .Get<LunchServiceSettings>();
            _lunchServiceApiKey = moviesConfig.ApiKey;
            _lunchServiceID = Guid.Parse(moviesConfig.ProjectId);

           // _lunchServiceApiKey = Configuration["CodeMash:ApiKey"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var client = new CodeMashClient(_lunchServiceApiKey, _lunchServiceID);
            
            var service = new CodeMashRepository<Person>(client);
            var person = new Person { Name = "Tim" };

            await service.InsertOneAsync(person, new DatabaseInsertOneOptions());

                       

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello");
                });
            });
        }
    }
}
