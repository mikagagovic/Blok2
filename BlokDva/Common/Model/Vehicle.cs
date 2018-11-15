using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class Vehicle
    {
        string name;
        string model;
        int year;
        double pricePerHour;
        double priceVehicle;
        bool available;

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
        public string Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }

        [DataMember]
        public int Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }

        [DataMember]
        public double PricePerHour
        {
            get
            {
                return pricePerHour;
            }

            set
            {
                pricePerHour = value;
            }
        }

        [DataMember]
        public double PriceVehicle
        {
            get
            {
                return priceVehicle;
            }

            set
            {
                priceVehicle = value;
            }
        }

        [DataMember]
        public bool Available
        {
            get
            {
                return available;
            }

            set
            {
                available = value;
            }
        }

        public Vehicle()
        {

        }

        public Vehicle(string name, string model, int year, double price, double cena, bool available)
        {
            this.Name = name;
            this.Model = model;
            this.Year = year;
            this.PricePerHour = price;
            this.priceVehicle = cena;
            this.Available = available;
        }
    }
}
