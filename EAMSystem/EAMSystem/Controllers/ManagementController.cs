using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using EAMSystem.Models;

namespace EAMSystem.Controllers
{
    public class ManagementController : Controller
    {
        //
        // GET: /Management/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Management/StudentList
   
        public ActionResult StudentList()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                return View(StudentManager.GetStudents());
            }
            else return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Management/EditStudent (admin)

        public ActionResult EditStudent(string id)
        {
            if(Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                return View(StudentManager.GetStudentById(id));
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditStudent(string id, string stuno, string name, string major, string className)
        {
            Student stu = StudentManager.GetStudentById(id);

            if (stu == null)
            {
                StudentManager.AddStudent(new Student { StuNo = stuno, Name = name, Major = major, Class = className });

                Membership.CreateUser(stuno, "123456");
                Roles.AddUserToRole(stuno, "Student");
            }
            else
            {
                stu.StuNo = stuno;
                stu.Name = name;
                stu.Major = major;
                stu.Class = className;
                StudentManager.Update(stu);
            }

            return RedirectToAction("StudentList", "Management");
        }

        //
        // GET: /Management/TeacherList (admin)

        public ActionResult TeacherList()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                return View(TeacherManager.GetTeachers());
            }
            else return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Management/EditTeacher (admin)

        public ActionResult EditTeacher(string id)
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                return View(TeacherManager.GetTeacherById(id));
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditTeacher(string id, string teano, string name, string institution)
        {
            Teacher tea = TeacherManager.GetTeacherById(id);

            if (tea == null)
            {
                TeacherManager.AddTeacher(new Teacher { TeaNo = teano, Name = name, Institution = institution });

                Membership.CreateUser(teano, "123456");
                Roles.AddUserToRole(teano, "Teacher");
            }
            else
            {
                tea.TeaNo = teano;
                tea.Name = name;
                tea.Institution = institution;

                TeacherManager.Update(tea);
            }

            return RedirectToAction("TeacherList", "Management");
        }

        //
        // GET: /Management/CourseList (admin)
        
        public ActionResult CourseList()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                return View(CourseManager.GetCourses());
            }
            else return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Management/EditCourse (admin)

        public ActionResult EditCourse(string id)
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                return View(CourseManager.GetCourseById(id));
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditCourse(string id, string courseno, string name, string period, string credit)
        {
            Course cou = CourseManager.GetCourseById(id);

            int cre, per;
            Int32.TryParse(credit, out cre);
            Int32.TryParse(period, out per);
            if (cou == null) CourseManager.AddCourse(new Course { CourseNo = courseno, Name = name, Credit = cre, Period = per });
            else
            {
                cou.CourseNo = courseno;
                cou.Name = name;
                cou.Period = per;
                cou.Credit = cre;

                CourseManager.Update(cou);
            }

            return RedirectToAction("CourseList", "Management");
        }
    }
}
