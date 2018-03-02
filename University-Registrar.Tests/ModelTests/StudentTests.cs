
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UniversityRegistrar.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace UniversityRegistrar.Tests
{
  [TestClass]
  public class StudentTests : IDisposable
  {
    public StudentTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_test;";
    }
    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }

    [TestMethod]
    public void Equals_OverrideTrueForSameName_Student()
    {
      DateTime dt = new DateTime(2000, 1, 1, 1, 0, 0);
      Student firstStudent = new Student("Faiza", 1, "Senior", dt);
      Student secondStudent = new Student("Faiza", 1, "Senior", dt);

      Assert.AreEqual(firstStudent, secondStudent);
    }
    [TestMethod]
    public void GetAll_DatabaseEmtpyAtFirst_0()
    {
    int result = Student.GetAll().Count;
    Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_StudentSavesToDatabase_StudentList()
    {
      DateTime dt = new DateTime(2000, 1, 1, 1, 0, 0);
      Student testStudent = new Student("Faiza", 1, "Senior", dt);
      testStudent.Save();

      List<Student> result = Student.GetAll();
      List<Student> testList = new List<Student>{testStudent};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      DateTime dt = new DateTime(2000, 1, 1, 1, 0, 0);
      Student testStudent = new Student("Jahmanz", 1, "Senior", dt);
      testStudent.Save();

      Student savedStudent = Student.GetAll()[0];

      int result = savedStudent.GetId();
      int testId = testStudent.GetId();

      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsStudentInDatabase_Student()
    {
      DateTime dt = new DateTime(2000, 1, 1, 1, 0, 0);
      Student testStudent = new Student("Jahmanz", 1, "Senior", dt);
      testStudent.Save();

      Student result = Student.Find(testStudent.GetId());

      Assert.AreEqual(testStudent, result);
    }
    [TestMethod]
    public void Update_UpdateStudentInDatabase_String()
    {
      DateTime dt = new DateTime(2000, 1, 1, 1, 0, 0);
      // string studentName = "Faizi";
      Student testStudent = new Student("Faizi", 1, "Senior", dt);
      testStudent.Save();
      string updateName = "Faiza";

      testStudent.UpdateName(updateName);

      string result = Student.Find(testStudent.GetId()).GetName();

      Assert.AreEqual(updateName, result);
    }
    [TestMethod]
    public void Add_AddCourseToStudents_Int()
    {
      DateTime dt = new DateTime(2000, 1, 1, 1, 0, 0);

      Course testCourse = new Course("Math200");
      testCourse.Save();

      Student testStudent = new Student("Faiza", 1, "Senior", dt);
      testStudent.Save();

      testStudent.AddCourse(testCourse);

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      //return just the id of the testStudent
      cmd.CommandText = @"SELECT student_id FROM schedule WHERE student_id = @testStudentId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@testStudentId";
      searchId.Value = testStudent.GetId();
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int result = 0;

      while(rdr.Read())
      {
        result = rdr.GetInt32(0);
      }
      Assert.AreEqual(result, testStudent.GetId());
    }

    [TestMethod]
    public void DeleteOne_DeleteOneStudentInDatabase_Int()
    {
      DateTime dt = new DateTime(2000, 1, 1, 1, 0, 0);
      Student firstStudent = new Student("Jahmanz", 1, "Senior", dt);
      firstStudent.Save();
      Student secondStudent = new Student("Faiza", 1, "Senior", dt);
      secondStudent.Save();

      string result = Student.Find(firstStudent.GetId()).GetName();
      Assert.AreEqual(firstStudent.GetName(), result);
      firstStudent.DeleteOne(firstStudent.GetId());

      Assert.AreEqual(1, Student.GetAll().Count);
    }
    [TestMethod]
    public void DeleteAll_DeleteAllStudentInDatabase_Int()
    {
      DateTime dt = new DateTime(2000, 1, 1, 1, 0, 0);
      Student firstStudent = new Student("Jahmanz", 1, "Senior", dt);
      firstStudent.Save();
      Student secondStudent = new Student("Faiza", 1, "Senior", dt);
      secondStudent.Save();
      string result = Student.Find(firstStudent.GetId()).GetName();
      Assert.AreEqual(firstStudent.GetName(), result);
      Student.DeleteAll();
      Assert.AreEqual(0, Student.GetAll().Count);
    }
  }
}
