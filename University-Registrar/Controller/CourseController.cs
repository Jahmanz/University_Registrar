using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System;

namespace UniversityRegistrar
{
  public class CourseController : Controller
  {
    [HttpGet("/courses")]
    public ActionResult CourseIndex()
    {
      List<Course> allCourses = Course.GetAll();
      return View(allCourses);
    }

    [HttpGet("/courses/new")]
    public ActionResult CourseCreateForm()
    {
      return View();
    }

    [HttpPost("/courses")]
    public ActionResult CourseCreate()
    {
      Course newCourse = new Course(Request.Form["course-name"]);
      newCourse.Save();

      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/courses/{id}")]
    public ActionResult CourseDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Course selectedCourse = Course.Find(id);

      List<Student> courseStudents = selectedCourse.GetStudents();

      List<Student> allStudents = Student.GetAll();

      model.Add("course", selectedCourse);
      model.Add("courseStudents", courseStudents);
      model.Add("allStudents", allStudents);

      return View(model);
    }

    [HttpPost("/courses/{courseId}/students/new")]
    public ActionResult AddStudent(int courseId)
    {
      Course course = Course.Find(courseId);
      Student student = Student.Find(Int32.Parse(Request.Form["student-id"]));
      course.AddStudent(student);

      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/courses/{courseId}/update")]
    public ActionResult CourseUpdateForm(int courseId)
    {
      Course thisCourse = Course.Find(courseId);

      return View("update", thisCourse);
    }

    [HttpPost("/courses/{courseId}/update")]
    public ActionResult CourseUpdate(int courseId)
    {
      Course thisCourse = Course.Find(courseId);
      thisCourse.UpdateName(Request.Form["new-name"]);

      return RedirectToAction("Index");
    }

    [HttpGet("/courses/{courseId}/delete")]
    public ActionResult DeleteOne(int courseId)
    {
      Course thisCourse = Course.Find(courseId);
      thisCourse.DeleteOne();

      return RedirectToAction("index");
    }
  }
}
