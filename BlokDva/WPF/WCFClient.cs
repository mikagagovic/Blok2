using Common;
using Common.Model;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public class WCFClient : ChannelFactory<IService>, IService, IDisposable
    {
        IService factory;
        string OU1 = "korisnik";
        string OU2 = "admin";

        public WCFClient(NetTcpBinding binding, EndpointAddress address, string cliNameCrt): base(binding, address)
        {
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertificateValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            this.Credentials.ClientCertificate.Certificate = CertificateManager.GetSingleCertificate(StoreName.My, StoreLocation.LocalMachine, cliNameCrt, OU1, OU2);
            factory = this.CreateChannel();
        }

        public bool AddVehicle(string name, string model, int year, double price, double cena)
        {
            try
            {
                bool result = factory.AddVehicle(name, model, year, price, cena);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int CanIBecomeGolden(User user)
        {
            try
            {
                int result = factory.CanIBecomeGolden(user);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public bool DeleteVehicle(Vehicle vehicle)
        {
            try
            {
                bool result = factory.DeleteVehicle(vehicle);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DoRequest(Request request)
        {
            try
            {
                bool result = factory.DoRequest(request);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EditVehicle(string name, string model, int year, double price, double cena, Vehicle vehicle)
        {
            try
            {
                bool result = factory.EditVehicle(name, model, year, price, cena, vehicle);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public UserRole LogIn(string username, string password, out User user)
        {
            user = null;

            try
            {
                UserRole res = factory.LogIn(username, password, out user);
                return res;
            }
            catch (Exception)
            {
                return UserRole.unauthenticated;
            }
        }

        public bool LogOut(User user)
        {
            try
            {
                bool result = factory.LogOut(user);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int MakeRent(User user, Vehicle vehicle, string endDate)
        {
            try
            {
                int result = factory.MakeRent(user, vehicle, endDate);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int ReturnBill(User user)
        {
            try
            {
                int result = factory.ReturnBill(user);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Request> ReturnRequest()
        {
            try
            {
                List<Request> requests = factory.ReturnRequest();
                return requests;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<User> ReturnUsers(User currentUser)
        {
            try
            {
                List<User> users = factory.ReturnUsers(currentUser);
                return users;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Vehicle> ReturnVehicle(int option)
        {
            try
            {
                List<Vehicle> vehicles = factory.ReturnVehicle(option);
                return vehicles;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
