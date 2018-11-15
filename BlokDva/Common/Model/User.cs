using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class User
    {
        string username;
        string password;
        string name;
        string lastname;
        bool isAdmin;
        bool isGolden;
        int numberOfVehicles;
        int numberPerDay;

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
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        [DataMember]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        [DataMember]
        public string Lastname
        {
            get
            {
                return lastname;
            }

            set
            {
                lastname = value;
            }
        }

        [DataMember]
        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }

            set
            {
                isAdmin = value;
            }
        }

        [DataMember]
        public bool IsGolden
        {
            get
            {
                return isGolden;
            }

            set
            {
                isGolden = value;
            }
        }

        [DataMember]
        public int NumberOfVehicles
        {
            get
            {
                return numberOfVehicles;
            }

            set
            {
                numberOfVehicles = value;
            }
        }

        [DataMember]
        public int NumberPerDay
        {
            get
            {
                return numberPerDay;
            }

            set
            {
                numberPerDay = value;
            }
        }

        public User()
        {

        }

        public User(string user, string pass, string name, string last, bool admin, bool golden, int numberVehicles, int numberDay)
        {
            this.Username = user;
            this.Password = pass;
            this.Name = name;
            this.Lastname = last;
            this.IsAdmin = admin;
            this.IsGolden = golden;
            this.NumberOfVehicles = numberVehicles;
            this.NumberPerDay = numberDay;
        }
    }
}
