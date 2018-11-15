using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class Bill
    {
        string username;
        int sum;

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
        public int Sum
        {
            get
            {
                return sum;
            }

            set
            {
                sum = value;
            }
        }

        public Bill()
        {

        }

        public Bill(string username, int sum)
        {
            this.Username = username;
            this.Sum = sum;
        }
    }
}
