using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System;

namespace UniversityRegistrar
{
  public class StudentController : Controller
  {
    [HttpGet("/students")]
    public ActionResult StudentIndex()
    {
      List<Student> allStudents = Student.GetAll();
      return View(allStudents);
    }

    [HttpGet("/students/new")]
    public ActionResult StudentCreateForm()
    {
      return View();
    }

    [HttpPost("/students")]
    public ActionResult StudentCreate()
    {
      string name = Request.Form["student-name"];
      string grade = Request.Form["student-grade"];
      DateTime enrollment_date = DateTime.Parse(Request.Form["student-date"]);
      Student newStudent = new Student(name, grade, enrollment_date);
      newStudent.Save();

      return RedirectToAction("StudentIndex");
    }

    [HttpGet("/students/{id}")]
    public ActionResult StudentDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Student selectedStudent = Student.Find(id);

      List<Course> studentCourses = selectedStudent.GetCourses();
      List<Course> allCourses = Course.GetAll();

      model.Add("student", selectedStudent);
      model.Add("studentCourses", studentCourses);
      model.Add("allCourses", allCourses);

      return View("StudentDetails",model);
    }

    [HttpPost("/students/{studentId}/courses/new")]
    public ActionResult AddCourse(int studentId)
    {
      Student student = Student.Find(studentId);
      Course course = Course.Find(Int32.Parse(Request.Form["course-id"]));
      student.AddCourse(course);

      return View("Success");
    }

    [HttpGet("/students/{id}/update")]
    public ActionResult StudentUpdateForm(int id)
    {
      Student thisStudent = Student.Find(id);

      return View("StudentUpdate", thisStudent);
    }

    [HttpPost("/students/{id}/update")]
    public ActionResult Update(int id)
    {
      string newName = Request.Form["newname"];
      Student thisStudent = Student.Find(id);

      thisStudent.UpdateName(newName);
      return RedirectToAction("StudentIndex");
    }
  }
}
