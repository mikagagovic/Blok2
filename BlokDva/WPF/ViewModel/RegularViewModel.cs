using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WPF.Command;
using WPF.View;

namespace WPF.ViewModel
{
    public class RegularViewModel : INotifyPropertyChanged
    {
        private List<Vehicle> list;
        public Vehicle selected;
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public RentVehicleCommand RentVehicleCommand { get; set; }

        public RegularViewModel(RegularView viewParam, NetTcpBinding binding, EndpointAddress address, string endDate)
        {
            this.RentVehicleCommand = new RentVehicleCommand(this, binding, address, endDate, viewParam);

            WCFClient client = new WCFClient(binding, address, ConnectionUser.Instance().CurrentUser.Username);
            List = client.ReturnVehicle(1);
        }

        public Vehicle Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Selected"));
            }
        }

        public List<Vehicle> List
        {
            get { return list; }
            set
            {
                list = value;
                OnPropertyChanged(new PropertyChangedEventArgs("List"));
            }
        }
    }
}
