using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderServer.Models.Divisions
{
    public class Country
    {
        private string name { get; set; }
        private string code { get; set; }

        public Country(string name,string code)
        {
            this.name = name;
            this.code = code;
        }
        public string Name
        {
            get { return this.name; } 
        }
        public string Code
        {
            get { return this.code; }
        }

    }
}
