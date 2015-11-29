using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace UNOService
{
    class DatabaseHelper
    {
        //fields and properties
        private MySqlConnection connection;

        //constructor:
        //connect to databese 
        public DatabaseHelper()
        {
            String connectionInfo = "server=localhost;" +
                                    "database=localvermeer;" +
                                    "user id=root;" +
                                    "password=;"
                                    + "connect timeout=30;";

            connection = new MySqlConnection(connectionInfo);
        }


        //methodes:
        public bool CheckUserName(string UserName)
        {
            connection.Open();
            string SQL = "SELECT COUNT(UserName) FROM USER WHERE UserName ='" + UserName + "'";
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int CountUser(string UserName)
        {
            connection.Open();
            string SQL = "SELECT COUNT(UserName) FROM USER";
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            return count;
        }

        public void InsertGuest(string Username, string Password)
        {
            connection.Open();
            string SQL = "INSERT INTO USER VALUES ('" + Username + "', '" + Password + "')";
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }


    }
}
