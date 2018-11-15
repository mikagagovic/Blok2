using Common;
using Common.Model;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Shapes;
using WPF.ViewModel;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        private string srvCertCN = "wcfservice";
        private string OU1 = "korisnik";
        private string OU2 = "admin";
        private NetTcpBinding binding;
        private X509Certificate2 srvCert;
        private EndpointAddress address;
        private WCFClient client;

        public AdminView()
        {
            InitializeComponent();

            SetProxy();

            client = new WCFClient(binding, address, ConnectionUser.Instance().CurrentUser.Username);
            this.DataContext = new AdminViewModel(this, binding, address);
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
            bool logout = client.LogOut(ConnectionUser.Instance().CurrentUser);

            if (logout)
            {
                ConnectionUser.Instance().CurrentUser = null;
                MainWindow main = new MainWindow();
                this.Close();
                main.ShowDialog();
            }
            else
                MessageBox.Show("You have to LogIn first to LogOut");
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            bool result = client.AddVehicle(nameTextBox.Text, modelTextBox.Text, Convert.ToInt32(yearTextBox.Text), Convert.ToDouble(priceTextBox.Text), Convert.ToDouble(cenaTextBox.Text));

            if (result)
            {
                MessageBox.Show("You have succesfully added new vehicle");
                this.DataContext = new AdminViewModel(this, binding, address);
            }
            else
                MessageBox.Show(String.Format("Vehicle with name: {0}, already exits in database", nameTextBox.Text));
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Vehicle)dataGrid.SelectedItem == null)
                return;

            Vehicle vehicle = (Vehicle)dataGrid.SelectedItem;

            nameTextBox.Text = vehicle.Name;
            modelTextBox.Text = vehicle.Model;
            yearTextBox.Text = vehicle.Year.ToString();
            priceTextBox.Text = vehicle.PricePerHour.ToString();
            cenaTextBox.Text = vehicle.PriceVehicle.ToString();
        }
    }
}
