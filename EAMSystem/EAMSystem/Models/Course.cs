using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace EAMSystem.Models
{
    public class Course
    {
        public ObjectId Id { get; set; }

        public string CourseNo { get; set; }

        public string Name { get; set; }

        public int Credit { get; set; }

        public int Period { get; set; }
    }

    public static class CourseManager
    {
        private static MongoCollection<Course> collection;

        static CourseManager()
        {
            collection = new MongoClient(ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString)
                .GetServer().GetDatabase("EAMSystem").GetCollection<Course>("Courses");

            collection.EnsureIndex("CourseNo");
        }

        public static Course GetCourseById(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            return collection.FindOneById(new ObjectId(id));
        }

        public static bool AddCourse(Course course)
        {
            var query = Query.EQ("CourseNo", course.CourseNo);

            if (collection.Find(query).Count() > 0) return false;
            else
            {
                collection.Insert<Course>(course);
                return true;
            }
        }

        public static Course GetCourseByNo(string no)
        {
            return collection.Find(new QueryDocument { { "CourseNo", no } }).SingleOrDefault<Course>();
        }

        public static void Update(Course course)
        {
            collection.Update(new QueryDocument { { "_id", course.Id } },
                new UpdateDocument { { "$set", 
                                         new BsonDocument {
                                         {"CourseNo", course.CourseNo },
                                         {"Name", course.Name},
                                         {"Period", course.Period}, 
                                         {"Credit", course.Credit}} 
                                         }});
        }

        public static Course[] GetCourses()
        {
            return collection.FindAll().SetHint("CourseNo_1").ToArray<Course>();
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