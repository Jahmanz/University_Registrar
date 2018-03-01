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

    [TestMethod]
    public void GetAll_DatabaseEmtpyAtFirst_0()
    {
      int result = Course.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SaveCoursesToDatabase_CourseList()
    {
      Course testCourse = new Course("Math200");
      testCourse.Save();

      List<Course> result = Course.GetAll();
      List<Course> testList = new List<Course>{testCourse};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Course testCourse = new Course("Math200");
      testCourse.Save();

      Course savedCourse = Course.GetAll()[0];

      int result = savedCourse.GetId();
      int testId = testCourse.GetId();

      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsCourseInDatabase_Course()
    {
      Course testCourse = new Course("Math200");
      testCourse.Save();

      Course result = Course.Find(testCourse.GetId());

      Assert.AreEqual(testCourse, result);
    }

    [TestMethod]
    public void Update_UpdateCourseInDatabase_String()
    {

      Course testCourse = new Course("Math200");
      testCourse.Save();
      string updateName = "Math300";

      testCourse.UpdateName(updateName);

      string result = Course.Find(testCourse.GetId()).GetName();

      Assert.AreEqual(updateName, result);
    }

    [TestMethod]
    public void DeleteOne_DeleteOneCourseInDatabase_Int()
    {

      Course firstCourse = new Course("Math200");
      firstCourse.Save();
      Course secondCourse = new Course("Math300");
      secondCourse.Save();

      string result = Course.Find(firstCourse.GetId()).GetName();
      Assert.AreEqual(firstCourse.GetName(), result);
      firstCourse.DeleteOne(firstCourse.GetId());

      Assert.AreEqual(1, Course.GetAll().Count);
    }

    [TestMethod]
    public void DeleteAll_DeleteAllCourseInDatabase_Int()
    {

      Course firstCourse = new Course("Math200");
      firstCourse.Save();
      Course secondCourse = new Course("Math300");
      secondCourse.Save();

      string result = Course.Find(firstCourse.GetId()).GetName();
      Assert.AreEqual(firstCourse.GetName(), result);
      Course.DeleteAll();
      
      Assert.AreEqual(0, Course.GetAll().Count);
    }
  }
}
