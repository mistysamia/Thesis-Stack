using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{

    public class Student 
    {
        public ObjectId id { get; set; }
        public string studentID { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string semester { get; set; }
        public string name { get; set; }
        public string contactNO { get; set; }
        public string department { get; set; }
        public string dp { get; set; }
    }

    public enum semesterTypeStudent
    {
        Y3S1, Y3S2, Y4S1, Y4S2,
        Y1S1, Y1S2, Y2S1, Y2S2, Alumni
    }
}