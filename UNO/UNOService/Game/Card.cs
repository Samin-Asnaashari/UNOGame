using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UNOService
{
	public class Card
	{
		public Card(string color)
		{
			this.Color = color;
		}

		[DataMember]
		public string Color
		{
			get;
			private set;
		}
	}
}
