using System.ComponentModel;
using System.Runtime.Serialization;

namespace UNOService
{
    [DataContract]
    public enum StatusCode
    {
        [EnumMember, Description("Username or password incorrect.")]
        LOGIN_INCORRECT,
        [EnumMember, Description("User already logged in.")]
        LOGIN_ALREADY,

        [EnumMember, Description("Username is taken.")]
        REGISTER_USERNAME_TAKEN,
        [EnumMember, Description("Password is too short! It should be at least 6 characters long.")]
        REGISTER_PASSWORD_TOO_SHORT,

        [EnumMember, Description("An unkown error occured.")]
        UNKOWN_ERROR,

        [EnumMember]
        SUCCESS
    }
}
