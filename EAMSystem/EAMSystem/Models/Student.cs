using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Options;

namespace EAMSystem.Models
{
    public class Student
    {
        public ObjectId Id { get; set; }

        public string StuNo { get; set; }

        public string Name { get; set; }

        public string Major { get; set; }

        public string Class { get; set; }
    }

    public static class StudentManager
    {
        private static MongoCollection<Student> collection;

        static StudentManager()
        {
            collection = new MongoClient("mongodb://localhost").GetServer().GetDatabase("EAMSystem").GetCollection<Student>("Students");

            collection.EnsureIndex("StuNo");
        }

        public static Student GetStudentById(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

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

        public static Student GetStudentByStuno(string no)
        {
            return collection.Find(new QueryDocument { { "StuNo", no } }).SingleOrDefault<Student>();
        }

        public static void Update(Student student)
        {
            var option = new DocumentSerializationOptions(false);
            option.SerializeIdFirst = false;

            collection.Update(new QueryDocument { { "_id", student.Id } },
                new UpdateDocument { { "$set", 
                                         new BsonDocument {
                                         {"StuNo", student.StuNo },
                                         {"Name", student.Name},
                                         {"Major", student.Major}, 
                                         {"Class", student.Class}} 
                                         }});
        }

        public static string[] GetAllMajor()
        {
            return collection.Distinct<string>("Major").ToArray<string>();
        }

        public static string[] GetAllClass()
        {
            return collection.Distinct<string>("Class").ToArray<string>();
        }

        public static Student[] GetStudentsByClass(string className)
        {
            return collection.Find(new QueryDocument { { "Class", className } }).ToArray<Student>();
        }

        public static Student[] GetStudents()
        {
            return collection.FindAll().SetHint("StuNo_1").ToArray<Student>();
        }
    }
}