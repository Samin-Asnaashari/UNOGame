using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UNOService
{
    [DataContract]
	class NumberCard : Card
	{
		NumberCard(string color, int number)
			: base(color)
		{
			this.Number = number;
		}

		[DataMember]
		public int Number
		{
			get;
			private set;
		}
	}
}
