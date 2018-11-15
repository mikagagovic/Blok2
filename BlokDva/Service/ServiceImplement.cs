using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using SecurityManager;
using System.Xml;

namespace Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceImplement : IService
    {
        private List<User> users = new List<User>();
        private List<Rent> rents = new List<Rent>();
        private List<Vehicle> vehicles = new List<Vehicle>();
        private List<Request> requests = new List<Request>(); 
        private List<Bill> bills = new List<Bill>();

        public void LoadLists()
        {
            XmlSerializer serializer;
            StreamReader reader;

            serializer = new XmlSerializer(typeof(List<User>));
            reader = new StreamReader(@"../../../users.xml");
            List<User> listUser = (List<User>)serializer.Deserialize(reader);
            users.AddRange(listUser);

            serializer = new XmlSerializer(typeof(List<Vehicle>));
            reader = new StreamReader(@"../../../vehicles.xml");
            List<Vehicle> listVehicle = (List<Vehicle>)serializer.Deserialize(reader);
            vehicles.AddRange(listVehicle);

            serializer = new XmlSerializer(typeof(List<Rent>));
            reader = new StreamReader(@"../../../rents.xml");
            List<Rent> listRent = (List<Rent>)serializer.Deserialize(reader);
            rents.AddRange(listRent);

            serializer = new XmlSerializer(typeof(List<Request>));                  
            reader = new StreamReader(@"../../../requests.xml");                
            List<Request> listRequest = (List<Request>)serializer.Deserialize(reader);
            requests.AddRange(listRequest);

            serializer = new XmlSerializer(typeof(List<Bill>));
            reader = new StreamReader(@"../../../bills.xml");
            List<Bill> listBill = (List<Bill>)serializer.Deserialize(reader);
            bills.AddRange(listBill);

            reader.Close();
        }

        private void SaveToXML(int option)
        {
            XmlSerializer serializer;
            StreamWriter writer;

            switch (option)
            {
                case 1:
                    serializer = new XmlSerializer(typeof(List<User>));
                    writer = new StreamWriter(@"../../../users.xml");
                    serializer.Serialize(writer, users);
                    writer.Close();

                    break;

                case 2:
                    serializer = new XmlSerializer(typeof(List<Vehicle>));
                    writer = new StreamWriter(@"../../../vehicles.xml");
                    serializer.Serialize(writer, vehicles);
                    writer.Close();

                    break;

                case 3:
                    serializer = new XmlSerializer(typeof(List<Rent>));
                    writer = new StreamWriter(@"../../../rents.xml");
                    serializer.Serialize(writer, rents);
                    writer.Close();

                    break;

                case 4:
                    serializer = new XmlSerializer(typeof(List<Request>));  
                    writer = new StreamWriter(@"../../../requests.xml");    
                    serializer.Serialize(writer, requests);
                    writer.Close();

                    break;

                case 5:
                    serializer = new XmlSerializer(typeof(List<Bill>)); 
                    writer = new StreamWriter(@"../../../bills.xml");   
                    serializer.Serialize(writer, bills);
                    writer.Close();

                    break;

                default:
                    break;
            }
        }

        public UserRole LogIn(string username, string password, out User user)
        {
            user = null;

            foreach (var item in users)
            {
                if (item.Username == username && item.Password == password)
                {
                    if (item.IsAdmin == true)
                    {
                        Audit.AnnotateEvent(String.Format("Succesfully login with username {0} and password {1}", username, password));
                        user = item;
                        return UserRole.admin;
                    }
                    else
                    {
                        Audit.AnnotateEvent(String.Format("Succesfully login with username {0} and password {1}", username, password));
                        user = item;
                        return UserRole.korisnik;
                    }
                }

            }

            Audit.AnnotateEvent(String.Format("Error while login with username {0} and password {1}", username, password));
            return UserRole.unauthenticated;
        }

        public bool LogOut(User user)
        {
            if (user == null)
                return false;
            else
                return true;
        }

        public List<User> ReturnUsers(User currentUser)
        {
            List<User> returnList = new List<User>();

            foreach (var item in users)
            {
                if (item.Username == currentUser.Username)
                    continue;

                returnList.Add(item);
            }

            return returnList;
        }


        public List<Vehicle> ReturnVehicle(int option)
        {
            if (option == 0)        //za admina
                return vehicles;    
            else
            {
                List<Vehicle> outputList = new List<Vehicle>(); //za usera
                foreach (var item in vehicles)                  
                {
                    if (item.Available == true)
                        outputList.Add(item);
                }

                return outputList;
            }
        }

        public bool AddVehicle(string name, string model, int year, double price, double cena)
        {
            Vehicle vehicle = vehicles.FirstOrDefault(x => x.Name == name);

            if (vehicle == null)
            {
                Vehicle vehicleAdd = new Vehicle(name, model, year, price, cena, true);
                vehicles.Add(vehicleAdd);
                SaveToXML(2);

                return true;
            }
            else
                return false;
        }

        public int CanIBecomeGolden(User user)
        {
            Request request = new Request(user.Username, user.IsGolden);   
            bool result = CheckRequest(request);   

            if (user.NumberOfVehicles < 5)  
            {                               
                WriteXMLGolden(String.Format("User with username: {0}, failed to meet the requirements for becoming golden membership. You need to rent at least 5 vehicles to become golden member. Date: {1}", user.Username, DateTime.Now.ToString()));

                return 0;                   
            }
            else
            {
                if (user.IsGolden)  
                {
                    if (user.NumberPerDay > 2)     
                    {                          
                        WriteXMLGolden(String.Format("User with username: {0}, failed to meet the requirements for becoming regular member. You need to return vehicles and have rent max 2 vehicles to become regular member. Date: {1}", user.Username, DateTime.Now.ToString())); 

                        return 1;               
                    }
                    else
                    {
                        if (result) 
                        {
                            requests.Add(request);  
                            SaveToXML(4); 

                            return 2;
                        }
                        else
                        {
                            WriteXMLGolden(String.Format("User with username: {0}, already sent request. Date: {1}", user.Username, DateTime.Now.ToString())); 

                            return 4;   
                        }
                    }
                }
                else 
                {
                    if (result) 
                    {
                        requests.Add(request); 
                        SaveToXML(4); 

                        return 3;
                    }
                    else
                    {
                        WriteXMLGolden(String.Format("User with username: {0}, already sent request. Date: {1}", user.Username, DateTime.Now.ToString())); 

                        return 4;   
                    }
                }
            }
        }

        private bool CheckRequest(Request request)
        {
            Request check = requests.FirstOrDefault(x => x.Username == request.Username && x.State == request.State);
            
            if (check == null)
                return true; 
            else
                return false;
        }

        public List<Request> ReturnRequest()
        {
            return requests; 
        }

        public bool DoRequest(Request request)
        {
            User user = users.FirstOrDefault(x => x.Username == request.Username); 
            Request check = requests.FirstOrDefault(x => x.Username == request.Username && x.State == request.State); 

            if (user == null && check == null) 
                return false; 
            else 
            {
                if (request.State == true) 
                {
                    user.IsGolden = false; 
                    WriteXMLGolden(String.Format("User with username: {0}, his golden membership was changed back to regular. Date: {1}", user.Username, DateTime.Now.ToString()));
                }
                else 
                {
                    user.IsGolden = true; 
                    WriteXMLGolden(String.Format("User with username: {0}, has got his golden membership. Date: {1}", user.Username, DateTime.Now.ToString())); 
                }

                requests.Remove(check); 

                SaveToXML(1); 
                SaveToXML(4); 

                return true;
            }
        }

        private void WriteXMLGolden(string output)
        {
            List<string> list = LoadXMLGolden(); 
            list.Add(output);                   

            XmlSerializer serializer;
            StreamWriter writer;

            serializer = new XmlSerializer(typeof(List<string>));
            writer = new StreamWriter(@"../../../zlatni_clanovi_info.xml");
            serializer.Serialize(writer, list);
            writer.Close();
        }

        private List<string> LoadXMLGolden() 
        {
            List<string> output = new List<string>();
            XmlSerializer serializer;
            StreamReader reader;

            serializer = new XmlSerializer(typeof(List<string>));
            reader = new StreamReader(@"../../../zlatni_clanovi_info.xml");
            List<string> list = (List<string>)serializer.Deserialize(reader);
            output.AddRange(list);
            reader.Close();

            return output;
        }

        public int MakeRent(User user, Vehicle vehicle, string endDate)
        {
            User userCheck = users.FirstOrDefault(x => x.Username == user.Username);
            Vehicle vehicleCheck = vehicles.FirstOrDefault(x => x.Name == vehicle.Name);

            if (endDate == "" || endDate == null)
                return 4;   //vreme nije setovano
            if (Convert.ToDateTime(endDate) < DateTime.Now)
                return 3;   //vreme kad se vraca vozilo je manje od trenutnog kad se iznajmljuje

            if (userCheck == null && vehicleCheck == null) 
                return 0; //baci gresku
            else
            {
                if (user.IsGolden) 
                {
                    if (user.NumberPerDay >= 5) 
                        return 1; //ne moze da iznajmi vise jer je zadovoljivo kriterijum vozila koje max moze iznajmiti
                    else
                        userCheck.NumberPerDay++; //povecaj mu broj vozila koje je iznajmio u toku dana
                }
                else
                {
                    if (user.NumberPerDay >= 2) 
                        return 1; //ne moze da iznajmi vise jer je zadovoljivo kriterijum vozila koje max moze iznajmiti
                    else
                        userCheck.NumberPerDay++; //povecaj mu broj vozila koje je iznajmio u toku dana
                }

                vehicleCheck.Available = false; 

                Rent rent = new Rent(userCheck, vehicleCheck, DateTime.Now, DateTime.Now);
                rents.Add(rent); 

                Bill billCheck = bills.FirstOrDefault(x => x.Username == userCheck.Username);

                if (billCheck == null)
                    throw new Exception();
                else
                {
                    if (userCheck.IsGolden)
                    {
                        int currentSum = Convert.ToInt32(vehicleCheck.PricePerHour) + (1 * Convert.ToInt32(vehicleCheck.PriceVehicle) / 100);

                        if (billCheck.Sum < currentSum)
                            return 5;
                        else
                            billCheck.Sum -= currentSum;
                    }
                        
                    else
                    {
                        int currentSum = Convert.ToInt32(vehicleCheck.PricePerHour) + (3 * Convert.ToInt32(vehicleCheck.PriceVehicle) / 100);

                        if (billCheck.Sum < currentSum)
                            return 5;
                        else
                            billCheck.Sum -= currentSum;
                    }
                }

                SaveToXML(1); 
                SaveToXML(2); 
                SaveToXML(3); 
                SaveToXML(5); 

                return 2;
            }
        }

        public bool DeleteVehicle(Vehicle vehicle)
        {
            Vehicle vehicleCheck = vehicles.FirstOrDefault(x => x.Name == vehicle.Name); 

            if (vehicleCheck == null)
                return false;
            else
            {
                if (vehicleCheck.Available == false)
                    return false;
                else
                {
                    vehicles.Remove(vehicleCheck);
                    SaveToXML(2);

                    return true;
                }
            }
        }

        public bool EditVehicle(string name, string model, int year, double price, double cena, Vehicle vehicle)
        {
            Vehicle vehicleCheck = vehicles.FirstOrDefault(x => x.Name == vehicle.Name); 

            if (vehicleCheck == null)
                return false;
            else
            {
                if (vehicleCheck.Available == false)
                    return false;
                else
                {
                    vehicleCheck.Name = name;
                    vehicleCheck.Model = model;
                    vehicleCheck.Year = year;
                    vehicleCheck.PricePerHour = price;
                    vehicleCheck.PriceVehicle = cena;

                    SaveToXML(2);

                    return true;
                }
            }
        }

        public int ReturnBill(User user)
        {
            Bill billCheck = bills.FirstOrDefault(x => x.Username == user.Username);

            if (billCheck == null)
                throw new Exception();
            else
            {
                return billCheck.Sum;
            }
        }
    }
}
