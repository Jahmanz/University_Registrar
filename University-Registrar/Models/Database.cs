using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using UniversityRegistrar;
using System;

namespace UniversityRegistrar.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
