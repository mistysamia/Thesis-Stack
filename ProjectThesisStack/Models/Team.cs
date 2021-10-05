using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{

    public class Team
    {
        public ObjectId id { get; set; }
        public string teamName { get; set; }
        public ObjectId[] studentIDs { get; set; }
        public ObjectId supervisorID { get; set; }
        public string dp { get; set; }
        public DateTime date { get; set; }
    }

}