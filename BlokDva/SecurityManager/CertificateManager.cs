using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class CertificateManager
    {
        public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName, string tpklijenta, string tpservera)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);

            foreach (X509Certificate2 cert in store.Certificates)
            {
                if (cert.SubjectName.Name.Equals(string.Format("CN={0}", subjectName)))
                    return cert;
                else if (cert.SubjectName.Name.Equals(subjectName))
                    return cert;
               
            }
            return null;
        }

        public static X509Certificate2 GetSingleCertificate(StoreName storeName, StoreLocation storeLocation, string srvCertCN, string OU1, string OU2)
        {
            string userCN = "CN=" + srvCertCN;
            string orgUn1 = "OU1=" + OU1;
            string orgUn2 = "OU2=" + OU2;

            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2 certificate = new X509Certificate2();
            List<X509Certificate2> certCollection = new List<X509Certificate2>();

            foreach (var cert in store.Certificates)
            {
                certCollection.Add(cert);
            }

            foreach (X509Certificate2 cert in certCollection)
            {
                string[] names = cert.Subject.Split(',');

                if (names[0] == userCN)
                {
                    certificate = cert;
                    break;
                }
            }

            return certificate;
        }
    }
}

