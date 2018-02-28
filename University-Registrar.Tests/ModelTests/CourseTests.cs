using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using UniversityRegistrar.Models;
using System.Data;

namespace UniversityRegistrar.Tests
{
  [TestClass]
  public class CourseTests : IDisposable
  {
    public CourseTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_test;";
    }
    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }

    [TestMethod]
    public void Equals_OverrideTrueForSameName_Course()
    {
      Course firstCourse = new Course("Math200");
      Course secondCourse = new Course("Math200");

      Assert.AreEqual(firstCourse, secondCourse);
    }
  }
}
