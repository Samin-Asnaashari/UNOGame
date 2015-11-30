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
        private string serverIP = "localhost";
        private string databaseName = "uno";
        private string userName = "root";
        private string password = "";
        private int timeOutSeconds = 30;
        public DatabaseHandler()
        {
            string connectionInfo = $"server = {serverIP}; database = {databaseName}; user id = {userName}; password = {password}; connect timeout = {timeOutSeconds};";
            connection = new MySqlConnection(connectionInfo);
        }

        public String GetPlayerInfo(String column, String username)
        {
            try
            {
                String info;
                connection.Open();
                MySqlCommand cmd;
                String sql = "SELECT " + column + " FROM `players` WHERE Username = '" + username + "'";
                cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                info = Convert.ToString(reader[0].ToString());

                if (info != null || info != "")
                    return info;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return "Invalid";
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
            try
            {
                String passwordDB;
                connection.Open();
                MySqlCommand cmd;
                String sql = "SELECT Password FROM `players` WHERE Username = '" + username + "'";
                cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                passwordDB = Convert.ToString(reader[0]);

                if (passwordDB != null || passwordDB != "")
                {
                    if (passwordDB == password)
                        return true;
                }
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
