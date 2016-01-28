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

        public void AddPlayerWon(string username)
        {
            try
            {
                connection.Open();
                string sql = "UPDATE `players` SET GamesWon =`GamesWon`+" + 1 + " WHERE `Username`='" + userName + "';";

                MySqlCommand cmd = new MySqlCommand(sql, connection);

                cmd.ExecuteNonQuery();

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

        public void AddGamesPlayed(string username)
        {
            try
            {
                connection.Open();
                string sql = "UPDATE `players` SET Gamesplayed =`Gamesplayed`+" + 1 + " WHERE `Username`='" + userName + "';";

                MySqlCommand cmd = new MySqlCommand(sql, connection);

                cmd.ExecuteNonQuery();

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

        public void InsertGamePlayed(Player p/*, int gameID,string username*/)
        {
            try
            {
                connection.Open();
                string sql = "INSERT INTO `savedgames` (`Game ID`, `Username`) VALUES (" + p.Game.GameID + ", '" + p.UserName + "');";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
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

        public int FindCard(Card c)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd1;
                String sql1 = "SELECT CardID FROM `card` WHERE Type = '" + c.Type + "' AND Color = '" + c.Color + "' AND Number =" + c.Number + ")";
                cmd1 = new MySqlCommand(sql1, connection);
                MySqlDataReader reader = cmd1.ExecuteReader();

                string cardID = reader.Read().ToString();

                return Convert.ToInt32(cardID);
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

        //public Card FindCard(int cardID)
        //{
        //    try
        //    {
        //        connection.Open();
        //        MySqlCommand cmd1;
        //        String sql1 = "SELECT CardID FROM `card` WHERE Type = '" + c.Type + "' AND Color = '" + c.Color + "' AND Number =" + c.Number + ")";
        //        cmd1 = new MySqlCommand(sql1, connection);
        //        MySqlDataReader reader = cmd1.ExecuteReader();

        //        string cardID = reader.Read().ToString();

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}

        public void InsertMove(int gameID, string username, Game.Move.Types moveType)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd;
                String sql = "INSERT INTO `move` (`ID`, `Username`, `GameID` , `Type`) VALUES ('', '" + userName + "' ," + gameID + " , '" + moveType + "')";
                cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
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

        public void InsertMove(int gameID, string username, int card, Game.Move.Types moveType)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd;
                String sql = "INSERT INTO `move` (`ID`, `Username`, `GameID` , `Type`, `Card`) VALUES ('', '" + userName + "' ," + gameID + " , '" + moveType + "' , '" + card + "')";
                cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
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

        //public void InsertMove(int gameID,string username,int cardID)
        //{
        //    try
        //    {
        //        connection.Open();
        //        MySqlCommand cmd;
        //        String sql = "INSERT INTO `moves` (`ID`, `Username`, `Game ID` , `Card`) VALUES ('', '" + userName + "' ," + gameID + " , " + cardID +")";
        //        cmd = new MySqlCommand(sql, connection);
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}

        public List<Game.Move> GetMoves(int gameID)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd;
                String sql = "SELECT Username,GameID,Type,Card FROM `move` WHERE GameID = " + gameID + "ORDER BY ID )";
                cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                string row = reader.Read().ToString();
                List<Game.Move> moves = new List<Game.Move>();

                string username;
                Card card;
                Game.Move.Types type;

                while (reader.Read())
                {
                    username = reader[0].ToString();
                    card = GetCard(Convert.ToInt32(reader[1]));
                    type = (Game.Move.Types)reader[2];
                    moves.Add(new Game.Move(username, gameID, card, type));
                }

                return moves;
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

        public Card GetCard(int cardID)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd;
                String sql = "SELECT Type,Color,Number FROM `card` WHERE CardID = " + cardID + ")";
                cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                string row = reader.Read().ToString();

                Game.CardType Type;
                Game.CardColor Color;
                int Number;
                Card c = null;
                while (reader.Read())
                {
                    Type = (Game.CardType)reader[0];
                    Color = (Game.CardColor)reader[1];
                    Number = Convert.ToInt32(reader[2]);
                    c = new Card(Type, Color, Number);
                }

                return c;
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

        public void InsertCard(Card card, int gameID)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd;
                String sql = "INSERT INTO `deck` (`ID`, `Type`, `Color`, `Number`) VALUES (" + gameID + ", '" + card.Type + "' ,'" + card.Color + "', " + card.Number + ");";
                cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
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

        public void InsertPlayers(List<Player> players)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd;
                foreach (var item in players)
                {
                    String sql = "INSERT INTO `gameinfo` (`GameID`, `PlayerUserName`) VALUES (" + item.Game.GameID + ", '" + item.UserName + "');";
                    cmd = new MySqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                }
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

        public List<Player> GetPlayersOfTheGame(int GameID)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd1;
                String sql1 = "SELECT PlayerUserName FROM `gameinfo` WHERE GameID = " + GameID + ";";
                cmd1 = new MySqlCommand(sql1, connection);
                MySqlDataReader reader = cmd1.ExecuteReader();

                string username = reader.Read().ToString();
                List<Player> players = new List<Player>();
                if (username != null)
                {
                    Player p = new Player(username);
                    players.Add(p);
                }
                while (reader.Read())
                {
                    username = reader.Read().ToString();
                    if (username != null)
                    {
                        Player p = new Player(username);
                        players.Add(p);
                    }
                }
                return players;
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

        public Stack<Card> GetDeck(int gameID)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd1;
                String sql1 = "SELECT Type,Color,Number FROM `deck` WHERE GameID = " + gameID + "ORDER BY GameID;";
                cmd1 = new MySqlCommand(sql1, connection);
                MySqlDataReader reader = cmd1.ExecuteReader();

                string row = reader.Read().ToString();
                Stack<Card> cards = new Stack<Card>();
                Game.CardType Type;
                Game.CardColor Color;
                int Number;
                while (reader.Read())
                {
                    Type = (Game.CardType)reader[0];
                    Color = (Game.CardColor)reader[1];
                    Number = Convert.ToInt32(reader[2]);
                    cards.Push(new Card(Type, Color, Number));
                }

                return cards;
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


        public List<int> GetSavedGame(string username)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd;
                String sql = "SELECT GameID FROM `savegame` WHERE Username = '" + username + "';";
                cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                string row = reader.Read().ToString();
                Stack<Card> cards = new Stack<Card>();

                List<int> id = new List<int>();

                while (reader.Read())
                {
                    id.Add(Convert.ToInt32(reader[0]));
                }

                return id;
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
