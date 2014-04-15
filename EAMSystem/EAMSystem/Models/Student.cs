using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace EAMSystem.Models
{
    public class Student
    {
        public ObjectId Id { get; set; }

        public string StuNo { get; set; }

        public string Name { get; set; }

        public string Major { get; set; }

        public int Class { get; set; }
    }

    public static class StudentManager
    {
        private static MongoCollection<Student> collection;

        static StudentManager()
        {
            collection = new MongoClient("mongodb:\\localhost").GetServer().GetDatabase("").GetCollection<Student>("Students");
        }

        public static Student GetStudentById(string id)
        {
            return collection.FindOneById(new ObjectId(id));
        }

        public static bool AddStudent(Student student)
        {
            var query = Query.EQ("StuNo", student.StuNo);

            if (collection.Find(query).Count() > 0) return false;
            else
            {
                collection.Insert<Student>(student);
                return true;
            }
        }
    }
}