using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNOService.Game;
using System.ServiceModel;

namespace UNOService
{
    public partial class UnoService : IReplay
    {
        public void SaveReplay(Player p)  //after game window 
        {
            //Player p = getPlayerFromGameContext();

            databaseHandler.InsertGamePlayed(p);
            databaseHandler.InsertPlayers(p.Game.Players);
            foreach (var item in p.Game.moves)
            {
                //databaseHandler.InsertMove(p.Game.GameID,p.UserName,item.Type);
                databaseHandler.InsertMove(p.Game.GameID, p.UserName, databaseHandler.FindCard(item.card), item.Type);
            }
            p.Game.InsertDeckIntoDatabase();
        }
    }
}
