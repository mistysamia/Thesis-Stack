using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{
    public class Notice
    {
        public ObjectId id { get; set; }
        public DateTime date { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string bodyLink { get; set; }
        public Boolean featured { get; set; }
        public string dp { get; set; }
    }
}