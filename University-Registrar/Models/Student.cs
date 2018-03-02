using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace UniversityRegistrar.Models
{
  public class Student
  {
    private int _id;
    private string _name;
    private string _grade;
    private DateTime _enrollment_date;

    public Student(string name, string grade, DateTime enrollment_date, int id = 0)
    {
      _name = name;
      _grade = grade;
      _enrollment_date = enrollment_date;
      _id = id;
    }

    public override bool Equals(System.Object otherStudent)
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
        bool gradeEquality = this.GetGrade() == newStudent.GetGrade();
        bool enrollment_dateEquality = this.GetEnrollmentDate() == newStudent.GetEnrollmentDate();

        return (idEquality && nameEquality && gradeEquality && enrollment_dateEquality);
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
    public void SetEnrollmentDate(DateTime enrollment_date)
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
          string grade = rdr.GetString(2);
          DateTime enrollment_date = rdr.GetDateTime(3);

          Student newStudent = new Student(studentName, grade, enrollment_date, studentId);
          allStudents.Add(newStudent);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allStudents;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students (name, grade, enrollment_date) VALUES (@name, @grade, @enrollment_date);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter grade = new MySqlParameter();
      grade.ParameterName = "@grade";
      grade.Value = this._grade;
      cmd.Parameters.Add(grade);

      MySqlParameter enrollment_date = new MySqlParameter();
      enrollment_date.ParameterName = "@enrollment_date";
      enrollment_date.Value = this._enrollment_date;
      cmd.Parameters.Add(enrollment_date);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
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
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int studentId = 0;
      string studentName = "";
      string grade = "";
      DateTime enrollmentDate = new DateTime (2000, 1, 1, 1, 0, 0);

      while (rdr.Read())
      {
        studentId = rdr.GetInt32(0);
        studentName = rdr.GetString(1);
        grade = rdr.GetString(2);
        enrollmentDate = rdr.GetDateTime(3);
      }
      Student newStudent = new Student(studentName, grade, enrollmentDate, studentId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStudent;
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
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddCourse(Course newCourse)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO schedule (student_id) VALUES (@studentId);";

      MySqlParameter student_id = new MySqlParameter();
      student_id.ParameterName = "@studentId";
      student_id.Value = _id;
      cmd.Parameters.Add(student_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Course> GetCourses()
    {
      List<Course> newCourseList = new List<Course>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT courses.* FROM students
                JOIN schedule ON (students.id = schedule.student_id)
                JOIN courses ON (schedule.course_id = courses.id)
                WHERE students.id = @StudentId;";

      MySqlParameter student_id = new MySqlParameter();
      student_id.ParameterName = "@StudentId";
      student_id.Value = _id;
      cmd.Parameters.Add(student_id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        string name = rdr.GetString(1);
        Course newCourse = new Course(name);
        newCourseList.Add(newCourse);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCourseList;
    }

    public void DeleteOne()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
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
      cmd.CommandText = @"DELETE FROM students;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
