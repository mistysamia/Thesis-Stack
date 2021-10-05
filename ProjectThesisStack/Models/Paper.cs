using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{

    public class Paper
    {
        public ObjectId id { get; set; }
        public string title { get; set; }
        public string body { get; set; } // abstract
        public ObjectId teamID { get; set; }
        public string[] tags { get; set; }
        public ObjectId interractionID { get; set; }
        public DateTime publicationDate { get; set; }
        public string[] publicationLinks { get; set; }
        public string status { get; set; }
        public string fileLink { get; set; }
    }

}