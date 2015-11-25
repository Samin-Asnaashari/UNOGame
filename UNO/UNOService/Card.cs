using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UNOService
{
	[DataContract]
	class Card
	{
		private string color;
		private int number;

		Card(string color, int number)
		{
			this.color = color;
			this.number = number;
		}

		[DataMember]
		public string Color { get; }

		[DataMember]
		public int Number { get; }
	}
}
