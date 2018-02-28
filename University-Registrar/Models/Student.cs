using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace UniversityRegistrar
{
  public class Student
  {
    private int _id;
    private string _name;
    private int _course_id;
    private string _grade;
    private DateTime _enrollment_date;

    public Student(string name, int course_id, string grade, DateTime enrollment_date, int id = 0)
    {
      _name = name;
      _course_id = course_id;
      _grade = grade;
      _enrollment_date = enrollment_date;
      _id = id;
    }

    public override bool Equals(Sustem.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool idEquality = this.GetId() == newStudent.GetId();
        bool nameEquality = this.GetName() == newStudent.GetName();
        bool courseEquality = this.GetCourseId() == newStudent.GetCourseId();
        bool gradeEquality = this.GetGrade() == newStudent.GetGrade();
        bool enrollment_dateEquality = this.GetEnrollmentDate() == newStudent.GetEnrollmentDate();

        return (idEquality && nameEquality && courseEquality && gradeEquality && enrollment_dateEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    //GETTERS & SETTERS
    public int GetId()
    {
      return _id;
    }
    public void SetId(int id)
    {
      _id = id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    public int GetCourseId()
    {
      return _course_id;
    }
    public void SetCourseId(int course_id)
    {
      _course_id = course_id;
    }
    public string GetGrade()
    {
      return _grade;
    }
    public void SetGrade(string grade)
    {
      _grade = grade;
    }
    public DateTime GetEnrollmentDate()
    {
      return _enrollment_date;
    }
    public void SetEnrollmentDate()
    {
      _enrollment_date = enrollment_date;
    }
    public static List<Student> GetAll()
    {
        List<Student> allStudents = new List<Student>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM students;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int studentId = rdr.GetInt32(0);
          string studentName = rdr.GetString(1);
          int course_id = rdr.GetInt32(2);
          string grade = rdr.GetString(3);
          int enrollment_date = rdr.GetDateTime(4);

          Student newStudent = new Student(studentName, course_id, grade, enrollment_date, studentId);
          allStudents.Add(newStudent);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allStudents;
    }
    public static Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students (name, course_id, grade, enrollment_date);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter course_id = new MySqlParameter();
      course_id.ParameterName = "@course_id";
      course_id.Value = this._course_id;
      cmd.Parameters.Add(course_id);

      MySqlParameter grade = new MySqlParameter();
      grade.ParameterName = "@grade";
      grade.Value = this._grade;
      cmd.Parameters.Add(grade);

      MySqlParameter enrollment_date = new MySqlParameter();
      enrollment_date.ParameterName = "@enrollment_date";
      enrollment_date.Value = this._enrollment_date;
      cmd.Parameters.Add(enrollment_date);

      cmd.ExecuteNonQuery();
      _id = (int) cdm.LastInsertedId;
      conn.Close();
      if (c;onn != null)
      {
        conn.Dispose();
      }
    }
    public static Student Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int studentId = 0;
      string studentName = "";
      int courseId = 0;
      string grade = "";
      DateTime enrollmentDate = 0;

      while (rdr.Read())
      {
        studentId = rdr.GetInt32(0);
        studentName = rdr.GetString(1);
        courseId = rdr.GetInt32(2);
        grade = rdr.GetString(3);
        enrollmentDate = rdr.GetDateTime(4);
      }
      Student newStudent = new Student(studentName, courseId, grade, enrollmentDate, studentId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE students SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName "@newName";
      name.Value = newName;
      cmd.Parameters.Add(newName);

      cmd.ExecuteNonQuery();
      _name = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void DeleteOne(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM student WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId"
      searchId.Value - _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE * FROM students;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
