using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using MongoDB.Driver;
using MongoDB.Bson;

namespace EAMSystem.Models
{
    public class Course
    {
        public ObjectId Id { get; set; }

        public string CourseNo { get; set; }

        public string Name { get; set; }

        public int Credit { get; set; }

        public int Period { get; set; }

        //------------------------------------

        private static MongoCollection<Course> collection;

        static Course()
        {
            collection = new MongoClient(ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString)
                .GetServer().GetDatabase("EAMSystem").GetCollection<Course>("Courses");
        }
    }

    public class CourseInArrangement
    {
        public ObjectId Id { get; set; }

        public string CourseNo { get; set; }

        public List<CourseSchedule> Schedule { get; set; }

        public CourseInArrangement()
        {
            this.Schedule = new List<CourseSchedule>();
        }

        //------------------------------------------------

        private static MongoCollection<CourseInArrangement> collection;

        static CourseInArrangement()
        {
            collection = new MongoClient(ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString)
                .GetServer().GetDatabase("EAMSystem").GetCollection<CourseInArrangement>("CourseInArrangement");
        }
    }
}