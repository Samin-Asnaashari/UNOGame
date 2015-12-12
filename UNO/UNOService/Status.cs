using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UNOService
{
    [DataContract]
    public struct StatusCode
    {
        private int code;
        private string status;

        public StatusCode(int code)
        {
            this.code = code;
            this.status = "";

            this.Code = code; //Call logic of 'Code'
        }

        public StatusCode(string status) //Only call this constructor when the status depends on a thrown clause
        {
            this.code = -99;
            this.status = status;
        }

        [DataMember]
        public string Status
        {
            get
            {
                return status;
            }
        }

        [DataMember]
        public int Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;

                switch(code)
                {
                    /* Errors: */
                    //Signup:
                    case -10:
                        status = "Username is taken";
                        break;
                    case -11:
                        status = "Password is too short";
                        break;
                    //Login:
                    case -20:
                        status = "User already logged in";
                        break;

                    /* Succeeds: */
                    //Registration:
                    case 10:
                        status = "Registration successful";
                        break;
                    //Login:
                    case 20:
                        status = "Login succesful";
                        break;


                    default:
                        status = "Unkown status code";
                        break;
                }
            }
        }
    }
}
