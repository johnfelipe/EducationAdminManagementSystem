using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAMSystem.Models
{
    public class Teacher
    {
        public ObjectId Id { get; set; }

        public string TeaNo { get; set; }

        public string Name { get; set; }

        public string Institution { get; set; }
    }

    public static class TeacherManager
    {
        private static MongoCollection<Teacher> collection;

        static TeacherManager()
        {
            collection = new MongoClient("mongodb://localhost").GetServer().GetDatabase("EAMSystem").GetCollection<Teacher>("Teachers");

            collection.EnsureIndex("TeaNo");
        }

        public static Teacher GetTeacherById(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            return collection.FindOneById(new ObjectId(id));
        }

        public static bool AddTeacher(Teacher teacher)
        {
            var query = Query.EQ("TeaNo", teacher.TeaNo);

            if (collection.Find(query).Count() > 0) return false;
            else
            {
                collection.Insert<Teacher>(teacher);
                return true;
            }
        }

        public static Teacher GetTeacherByTeano(string no)
        {
            return collection.Find(new QueryDocument { { "TeaNo", no } }).SingleOrDefault<Teacher>();
        }

        public static void Update(Teacher teacher)
        {
            collection.Update(new QueryDocument { { "_id", teacher.Id } },
                new UpdateDocument { { "$set", 
                                         new BsonDocument {
                                         {"TeaNo", teacher.TeaNo },
                                         {"Name", teacher.Name},
                                         {"Institution", teacher.Institution}
                                         }}});
        }

        public static Teacher[] GetTeachers()
        {
            return collection.FindAll().SetHint("TeaNo_1").ToArray<Teacher>();
        }
    }
}