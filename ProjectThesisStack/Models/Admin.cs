using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{

    public class Admin
    {
        public ObjectId id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string contactNO { get; set; }
        public string dp { get; set; }

    }

}