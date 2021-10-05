using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{



    public class Supervisor
    {
        public ObjectId id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string contactNO { get; set; }
        public string[] educationalBackgrounds { get; set; }
        public string roomNo { get; set; }
        public string[] honorsAndAchievements { get; set; }
        public string[] researchInterests { get; set; }
        public int totalSlot { get; set; }
        public int remainingSlot { get; set; }
        public string dp { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
    }



}