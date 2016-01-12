using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UNOService.Game;

namespace UNOService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class UnoService : ILoginAndSignUp
    {
        private static int gameID = 0;
        private DatabaseHandler databaseHandler;

        private List<Player> playersOnline = new List<Player>();
        private List<Game.Game> games = new List<Game.Game>();

        public UnoService()
        {
            this.AllPlayersConnected += UnoServiceGame_AllPlayersConnected;
            databaseHandler = new DatabaseHandler();
        }

        public StatusCode Login(string userName, string password)
        {
            bool loginSuccessful = false;
            try
            {
                if (playersOnline.Find(x => x.UserName.ToLower() == userName.ToLower()) == null) //user not logged in yet
                {
                    loginSuccessful = databaseHandler.CheckLogin(userName, password);
                    if (loginSuccessful)
                        CreatePlayer(userName);
                    else
                        return StatusCode.LOGIN_INCORRECT; //Credentials incorrect
                }
                else
                {
                    return StatusCode.LOGIN_ALREADY; //User already logged in
                }
            }
            catch
            {
                return StatusCode.UNKOWN_ERROR; //TODO: Maybe pass the exception with the statuscode in some way? I'm not sure if enums can do this
            }

            return StatusCode.SUCCESS;
        }

        private void CreatePlayer(string username) //method maybe not needed
        {
            Player toBeAdded = new Player(username);
            toBeAdded.State = PlayerState.InLobby;
            playersOnline.Add(toBeAdded);
        }

        public StatusCode SignUp(string userName, string password)
        {
            if (CheckUserName(userName))
                return StatusCode.REGISTER_USERNAME_TAKEN;

            if (password.Length < 6)
                return StatusCode.REGISTER_PASSWORD_TOO_SHORT;

            try
            {
                databaseHandler.InsertPlayer(userName, password);
                CreatePlayer(userName);
                return StatusCode.SUCCESS;
            }
            catch
            {
                return StatusCode.UNKOWN_ERROR;  //TODO: Maybe pass the exception with the statuscode in some way? I'm not sure if enums can do this
            }
        }

        public bool CheckUserName(string userName)
        {
            bool exist;

            try
            {
                exist = databaseHandler.CheckUserName(userName);
            }
            catch (Exception)
            {
                exist = false;
            }

            return exist;
        }

        /// <summary>
        /// Username could be null if he logged out, so use this try method for safety.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool tryGetPlayerFromUsername(string username, out Player player)
        {
            player = playersOnline.FirstOrDefault(x => x.UserName == username);

            return player != null;
        }
    }
}
