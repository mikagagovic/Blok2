using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
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
    /// Interaction logic for RegularView.xaml
    /// </summary>
    public partial class RegularView : Window
    {
        private string srvCertCN = "wcfservice";
        private string OU1 = "korisnik";
        private string OU2 = "admin";
        private NetTcpBinding binding;
        private X509Certificate2 srvCert;
        private EndpointAddress address;
        private WCFClient client;
        private string endDate;

        public RegularView()
        {
            InitializeComponent();

            SetProxy();
            client = new WCFClient(binding, address, ConnectionUser.Instance().CurrentUser.Username);

            this.DataContext = new RegularViewModel(this, binding, address, endDate);

            int result = client.ReturnBill(ConnectionUser.Instance().CurrentUser);
            billTextBox.Text = result.ToString();
        }

        private void SetProxy()
        {
            binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            srvCert = CertificateManager.GetSingleCertificate(StoreName.My, StoreLocation.LocalMachine, srvCertCN, OU1, OU2);
            address = new EndpointAddress(new Uri("net.tcp://localhost:4000/IService"), new X509CertificateEndpointIdentity(srvCert));
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker; //ova funkcija vadi vreme iz  kalendara
            DateTime? date = picker.SelectedDate;

            if (date == null)
                MessageBox.Show("Greska sa datumom");
            else
                endDate = date.ToString();

            this.DataContext = new RegularViewModel(this, binding, address, endDate);
        }

        private void ButtonGolden_Click(object sender, RoutedEventArgs e)
        {
            int result = client.CanIBecomeGolden(ConnectionUser.Instance().CurrentUser);

            if (result == 0)
                MessageBox.Show("You dont have enough rented vehicles, you need to rent at least 5 vehicles");
            else if (result == 1)
                MessageBox.Show("You as golden member to become a regular member have to return some vehicles");
            else if (result == 2)
                MessageBox.Show("You have succesfully send request to become regular member");
            else if (result == 3)
                MessageBox.Show("You have succesfully send request to become golden member");
            else
                MessageBox.Show("You have already sent request");
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
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
    }
}
