using CodeMash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LunchOrderServer.Models.Persons
{
    [CollectionName("persons")]
    public class Person : Entity
    {
        public string Name { get; set; }
        public string OneMoreToCheck { get; set; }
    }
}
