using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{

    public class Interraction
    {
        public ObjectId id { get; set; }
        public Reacts reacts { get; set; }
        public Comment[] comments { get; set; }
    }
}