using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        UserRole LogIn(string username, string password, out User user);

        [OperationContract]
        bool LogOut(User user);

        [OperationContract]
        List<User> ReturnUsers(User currentUser);


        [OperationContract]
        List<Vehicle> ReturnVehicle(int option);

        [OperationContract]
        bool AddVehicle(string name, string model, int year, double price, double cena);

        [OperationContract]
        int CanIBecomeGolden(User user);

        [OperationContract]
        List<Request> ReturnRequest();

        [OperationContract]
        bool DoRequest(Request request);

        [OperationContract]
        int MakeRent(User user, Vehicle vehicle, string endDate);

        [OperationContract]
        bool DeleteVehicle(Vehicle vehicle);

        [OperationContract]
        bool EditVehicle(string name, string model, int year, double price, double cena, Vehicle vehicle);

        [OperationContract]
        int ReturnBill(User user);
    }

    public enum UserRole
    {
        korisnik = 0,
        admin,
        unauthenticated
    }
}
