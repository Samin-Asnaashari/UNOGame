using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UNOService.Game;

namespace UNOService
{
    [DataContract]
	public class Card
	{
        public CardType Type { get; set; }
        public int Number { get; set; }

        public CardColor Color { get; set; }

        public Card(CardType Type, CardColor Color, int Number)
        {

        }


    }
}
