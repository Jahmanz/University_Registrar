using System.Collections.Generic;
using UniversityRegistrar.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UniversityRegistrar
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Course> allCourses = Course.GetAll();
      return View();
    }

    [HttpGet("/Home/Success")]
    public ActionResult Success()
    {
      return View();
    }
  }
}
