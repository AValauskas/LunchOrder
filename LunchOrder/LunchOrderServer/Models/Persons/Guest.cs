using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderServer.Models.Persons
{
    public class Guest
    {
        public string Name { get; set; }
        public string Id_Guest { get; set; }

        public Guest(string name, string id_guest)
        {
            this.Name = name;
            this.Id_Guest = id_guest;
        }
    }
}
