using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class ServiceCertificateValidator : X509CertificateValidator
    {
       string OU1 = "korisnik";
       string OU2 = "admin";

        public override void Validate(X509Certificate2 certificate)
        {
            string[] CN = certificate.SubjectName.Name.Split(',');

            X509Certificate2 cert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, certificate.SubjectName.Name, OU1, OU2);
            if (cert == null)
            {
                Audit.AnnotateEvent(String.Format("Error while login"));
                return;
            }

            if (!certificate.Issuer.Equals(cert.Issuer))
            {
                Audit.AnnotateEvent("Authentication failed. Certificate is not from a valid issuer.\n");
                throw new Exception("Certificate is not from a valid issuer.\n");
            }
            else if (certificate.NotAfter <= DateTime.Now)
            {
                Audit.AnnotateEvent("Authentication failed. Certificate has expired.\n");
                throw new Exception("Certificate has expired.\n");
            }
        }
    }
}
