using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UNOService
{
	class AttackingCard : Card
	{
		AttackingCard(string color, string type)
			: base(color)
		{
			this.Type = type;
		}

		[DataMember]
		public string Type
		{
			get;
			private set;
		}
	}
}
