using CodeMash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderServer.Models.Persons
{
    [CollectionName("Guests")]
    public class Guest : Entity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guest(string name)
        {
            this.Name = name;
        }
    }
}
