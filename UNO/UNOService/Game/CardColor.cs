using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UNOService.Game
{
    [DataContract]
    public enum CardColor
    {
        [EnumMember]
        None,
        [EnumMember]
        Red,
        [EnumMember]
        Green,
        [EnumMember]
        Blue,
        [EnumMember]
        Yellow
    }
}
