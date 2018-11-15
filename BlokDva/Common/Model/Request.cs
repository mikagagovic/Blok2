using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class Request
    {
        string username;
        bool state;

        [DataMember]
        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        [DataMember]
        public bool State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public Request()
        {

        }

        public Request(string username, bool state)
        {
            this.Username = username;
            this.State = state;
        }
    }
}
