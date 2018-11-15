using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WPF;
using SecurityManager;
using System.Security.Cryptography.X509Certificates;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceImplement serviceImplement = new ServiceImplement();
            serviceImplement.LoadLists();

            InitializeService(serviceImplement);

            Console.ReadLine();
        }

        private static void InitializeService(ServiceImplement service)
        {
            string servNameCrt = "wcfservice";
            string OU1 = "korisnik";
            string OU2 = "admin";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string address = "net.tcp://localhost:4000/IService";

            ServiceHost host = new ServiceHost(service);
            
            ServiceSecurityAuditBehavior newAuditBehavior = new ServiceSecurityAuditBehavior();
            host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
            host.Description.Behaviors.Add(newAuditBehavior);
            host.AddServiceEndpoint(typeof(IService), binding, address);

            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertificateValidator();
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            host.Credentials.ServiceCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, servNameCrt, OU1, OU2);

            host.Open();

            Console.WriteLine("WCFService is opened. Press <enter> to finish...");

        }
    }
}
