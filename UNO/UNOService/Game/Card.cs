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
        [DataMember]
        public CardType Type { get; set; }

        [DataMember]
        public CardColor Color { get; set; }

        [DataMember]
        public int Number { get; set; }


        public Card(CardType Type, CardColor Color, int Number)
        {
            this.Type = Type;
            this.Color = Color;
            this.Number = Number;
        }


    }
}
