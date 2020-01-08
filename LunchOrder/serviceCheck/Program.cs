using System;
using CodeMash.Client;
using CodeMash.Project.Services;
//using CodeMash.Models.Entities;
using Isidos.CodeMash.ServiceContracts;

namespace serviceCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Get your Project ID and Secret Key
            var projectId = Guid.Parse("4a988807-b77f-4518-8e4a-d59eaa4da592");
            var apiKey = "mPU0sDoCkK6ZTJEzRXGSAxO_9NqLtxp_";

            // 2. Create a general client for API calls
            var client = new CodeMashClient(apiKey, projectId);




            var projectService = new CodeMashProjectService(client);

          //  var settings = await projectService.GetProjectAsync(new GetProjectRequest());
        }
    }
}
