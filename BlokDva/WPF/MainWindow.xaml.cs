using Common;
using Common.Model;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.View;
using WPF.ViewModel;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string srvCertCN = "wcfservice";
        private string OU1 = "korisnik";
        private string OU2 = "admin";
        private WCFClient client;
        private NetTcpBinding binding;
        private X509Certificate2 srvCert;
        private EndpointAddress address;

        public MainWindow()
        {
            Thread.Sleep(1000);
            InitializeComponent();

            SetProxy();

            this.DataContext = new MainWindowViewModel(this, binding, address);
        }

        private void SetProxy()
        {
            binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            srvCert = CertificateManager.GetSingleCertificate(StoreName.My, StoreLocation.LocalMachine, srvCertCN, OU1, OU2);
            address = new EndpointAddress(new Uri("net.tcp://localhost:4000/IService"), new X509CertificateEndpointIdentity(srvCert));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text == "" || passwordBox.Password == "")
            {
                MessageBox.Show("None field can be empty!");
                return;
            }

            User user = null;
            client = new WCFClient(binding, address, usernameTextBox.Text);

            UserRole result = client.LogIn(usernameTextBox.Text, passwordBox.Password, out user);
            if (result == UserRole.unauthenticated)
            {
                MessageBox.Show(String.Format("User with username: {0}, does not exits or username/password combination is not right", usernameTextBox.Text));
                Audit.AnnotateEvent(String.Format("Error while login with username {0} and password {1}", usernameTextBox.Text, passwordBox.Password));
            }
            else
                ConnectionUser.Instance().CurrentUser = user;

            if (ConnectionUser.Instance().CurrentUser != null)
            {
                if (result == UserRole.admin)
                {
                    AdminView viewNew = new AdminView();
                    this.Close();
                    viewNew.ShowDialog();
                }
                else if (result == UserRole.korisnik)
                {
                    RegularView viewNew = new RegularView();
                    this.Close();
                    viewNew.ShowDialog();
                }
                else
                    return;
            }
        }
    }
}
