using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class Rent
    {
        User user;
        Vehicle vehicle;
        DateTime startTime;
        DateTime endTime;

        [DataMember]
        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        [DataMember]
        public Vehicle Vehicle
        {
            get
            {
                return vehicle;
            }

            set
            {
                vehicle = value;
            }
        }

        [DataMember]
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
            }
        }

        [DataMember]
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                endTime = value;
            }
        }

        public Rent()
        {

        }

        public Rent(User user, Vehicle vehicle, DateTime start, DateTime end)
        {
            this.User = user;
            this.Vehicle = vehicle;
            this.StartTime = start;
            this.EndTime = end;
        }
    }
}
