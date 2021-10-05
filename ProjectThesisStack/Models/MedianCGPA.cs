using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectThesisStack.Models
{

    public class MedianCGPA
    {
        public ObjectId id { get; set; }
        public string department { get; set; }
        public string session { get; set; }
        public string semester { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public double medianCG { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public double maxCG { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int totalStudents { get; set; }
    }

    public enum departmentType
    {
        CSE, EEE, ME, CE, TE, IPE
    }
    public enum semesterType
    {
        Y3S1, Y3S2, Y4S1, Y4S2
    }
}