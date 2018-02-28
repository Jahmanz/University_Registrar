using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace UniversityRegistrar
{
  public class Course
  {
    private int _id;
    private string _name;

    public Course(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }
    public override bool Equals(System.Object otherCourse)
    {
      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = this.GetId() == newCourse.GetId();
        bool nameEquality = this.GetName() == newCourse.GetName();

        return (idEquality && nameEquality);
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
      _name = name;
    }
    public void SetName()
    {
      _name = name;
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course>{};
      MySqlConnection = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT  * FROM courses;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);

        Course newCourse = new Course(courseName, courseId);
        allCourses.Add(newCourse);
      }
    }

    public static Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO courses (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Course Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int courseId = 0;
      string courseName = "";

      while(rdr.Read())
      {
        courseId = rdr.GetInt32(0);
        courseName = rdr.GetString(1);

        Course newCourse = new Course(courseName, courseId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newCourse;
      }
    }

    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE courses SET name = @newName WHERE id = @searchId;";

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

    public void DeleteOne()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn ! = null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = cpnn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE * FROM courses;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
