using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UNOService
{
    public class DatabaseHandler
    {
        private MySqlConnection connection;
        public DatabaseHandler()
        {
            string connectionInfo = "server = 127.0.0.1;" + "database = uno;" + "user id = root;" + "password = ;" + "connect timeout = 30;";
            connection = new MySqlConnection(connectionInfo);
        }
        public void InsertPlayerWon(string username)
        {

        }

        public void InsertGamesPlayed(string username)
        {

        }

        public void InsertPlayer(String username, String password)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd;
                String sql = "INSERT INTO `players` (`Username`, `Password`, `GamesWon`, `GamesPlayed`) VALUES ('" + username + "', '" + password + "', NULL, NULL);";
                cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void InsertMove()
        {

        }

        public bool CheckLogin(string username, string password)
        {
            return true;
        }

        public bool CheckUserName(string username)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd;
                String sql = "SELECT count(*) FROM `players` where Username = '" + username + "'";
                cmd = new MySqlCommand(sql, connection);
                int numberRows = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
                if (numberRows == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
