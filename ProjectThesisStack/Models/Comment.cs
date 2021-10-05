using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{
    public class Comment
    {
        public ObjectId userId { get; set; }
        public string body { get; set; }
        public int upCount { get; set; }
        public int downCount { get; set; }
    }
}